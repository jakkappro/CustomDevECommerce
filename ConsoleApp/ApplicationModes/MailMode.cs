﻿using Common.Interfaces;
using ConsoleApp.Mappers;
using ConsoleApp.Poco;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.Interfaces;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Interfaces;
using PohodaConnector.Interfaces;
using System.Reflection.Metadata;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ConsoleApp.ApplicationModes
{
    public class MailMode : IStarterService
    {
        private const int _lookBackPohoda = 15;

        private readonly int _lookBackDays;
        private readonly IExpandoOrder _expandoService;
        private readonly IMailSender _mailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailMode> _logger;
        private readonly ICarrier _carrier;
        private readonly IIdGenerator _generator;
        private readonly IOrderService _orderService;
        private readonly IStockService _stock;

        private Data data;

        private bool readOnly;

        public MailMode(int lookBackDays, IExpandoOrder expandoService, IMailSender mailService,
            IConfiguration configuration, ILogger<MailMode> logger, ICarrier carrier, IIdGenerator generator,
            IOrderService orderService, IStockService stock, bool readOnly)
        {
            _lookBackDays = lookBackDays;
            _expandoService = expandoService;
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _carrier = carrier;
            _generator = generator;
            _orderService = orderService;
            _stock = stock;
            this.readOnly = readOnly;
        }

        public void Run()
        {

            _mailService.Initialize(_configuration["Email:Login"], _configuration["Email:Recipient"],
                _configuration["Email:Cc"], _configuration["Email:Password"]);
            _logger.LogInformation("Mail service initialized.");

            _mailService.LoadTemplatesFromFile();

            _orderService.Initialize(_lookBackPohoda).Wait();
            _logger.LogInformation("Order service initialized.");

            _stock.Initialize().Wait();
            _logger.LogInformation("Stock service initialized.");

            var orders = _expandoService.GetExpandoOrders(_lookBackDays).order;
            _logger.LogInformation("Expando orders {ordersCount} successfully loaded.", orders.Count());

            var items = _expandoService.GetPrehomeItems().SHOPITEM.ToList();
            AddMissingItems(items);

            _logger.LogInformation("Prehome items {itemsCount} successfully loaded.", items.Count);

            foreach (var order in orders.OrderByDescending(o => o.orderStatus).ThenByDescending(o => o.purchaseDate))
            {
                data = new Data();
                if (order.orderStatus == "Unshipped")
                {

                    _logger.LogDebug("Checking order with id {id}", order.orderId);
                    if (_orderService.Exist(order.orderId))
                    {
                        EnsurePohodaOrderItemsExist(order, items).Wait();
                        _logger.LogInformation("Found order in pohoda, code: {code}", order.orderId);
                        continue;
                    }

                    var id = _generator.GetNextId();
                    _logger.LogInformation($"Id of new order: {id}");

                    data.PohodaOrderId = id;
                    data.AmazonOrderId = order.orderId;
                    data.Status = "Unshipped";

                    CreatePohodaOrder(order, id, items).Wait();

                    //CreateCarrierPackage(order, id).Wait();

                    AddToMail(order, items, id);

                    if (!readOnly)
                    {
                        using var db = new LiteDatabase(_configuration["Database:Path"]);
                        var col = db.GetCollection<Data>("customers");

                        col.Insert(data);

                        col.EnsureIndex(x => x.AmazonOrderId);
                        col.EnsureIndex(x => x.PohodaOrderId);
                        col.EnsureIndex(x => x.InternalPackageId);
                    }
                }
            }

            _logger.LogInformation("Sending mail");
            _mailService.SendMail();
        }

        private async Task CreatePohodaOrder(GetExpandoFeedRequest.ordersOrder order, string id,
            IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            EnsurePohodaOrderItemsExist(order, items).Wait();

            try
            {
                _logger.LogDebug($"Creating order {JsonSerializer.Serialize(ExpandoToPohodaOrder.Map(order, id, items))}");
                if (!readOnly)
                {
                    try
                    {
                        var mapOrder = ExpandoToPohodaOrder.Map(order, id, items);
                        if (mapOrder != null)
                        {
                            await _orderService.CreateOrder(mapOrder);
                        }
                        else
                        {
                            _logger.LogWarning($"Mapping of order returns null");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error while creating an order {order.orderId} in Pohoda. Country is {order.customer.address.country}");
                        return;
                    }
                }
                _logger.LogInformation($"Order with {id} sucessfully created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreatePohodaOrder failed with error {ex.Message}");
            }
        }

        private async Task EnsurePohodaOrderItemsExist(GetExpandoFeedRequest.ordersOrder order,
            IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            try
            {
                foreach (var stock in order.items)
                {
                    if (await _stock.Exists(stock.itemId.ToString()))
                    {
                        _logger.LogInformation("Found stock, id: {id}", stock.itemId);
                        continue;
                    }

                    var expandoItem = items.FirstOrDefault(e => e.ITEM_ID == stock.itemId);
                    if (expandoItem == null)
                    {
                        _logger.LogError("Cannot found expando item with id: {id}", stock.itemId);
                        continue;
                    }

                    _logger.LogDebug($"Creating stock {JsonSerializer.Serialize(ExpandoItemToPohodaStock.Map(expandoItem))}");
                    if (!readOnly)
                    {
                        _stock.CreateStock(ExpandoItemToPohodaStock.Map(items.First(e => e.ITEM_ID == stock.itemId)));
                    }
                    _logger.LogInformation($"Stock with id {stock.itemId} sucessfully created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"EnsurePohodaOrderItemsExists failed with message {ex.Message}");
            }
        }

        private async Task CreateCarrierPackage(GetExpandoFeedRequest.ordersOrder order, string id)
        {
            var packetData = ExpandoToPacketaPacket.Map(order, id);
            
            try
            {
                if (!readOnly)
                {
                    var packetId = await _carrier.CreatePackage(packetData);
                    data.LabelId = packetId;
                    _logger.LogInformation("Created packet, pohodaId: {id}, packetId: {packetId}", id, packetId);
                    Thread.Sleep(5000);
                    var res = await _carrier.GetLabel(packetId);
                    _logger.LogInformation("Generated label, name: {name}", res);
                    _mailService.AddAttachment(res);
                    var i = res.Split(".").First();
                    using var db = new LiteDatabase(_configuration["Database:Path"]);
                    var storage = db.GetStorage<string>();
                    storage.Upload(i, $"{_configuration["Packeta:LabelsLocation"]}/{res}");
                    data.LabelId = data.PohodaOrderId;
                }
            }
            catch (ArgumentException)
            {
                _logger.LogWarning("Skipping package: {id}", id);
            }

            data.CarrierInfo = order.customer.address.country switch
            {
                "DE" => "DPD",
                "AT" => "ATPOST",
                _ => $"Other"
            };
            data.DateCreated = DateTime.Now;
        }

        private void AddToMail(GetExpandoFeedRequest.ordersOrder order, IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items,
            string pohodaId)
        {
            _logger.LogDebug("Adding orderService id: {orderId}, with items {items} to mail.", order.orderId,
                order.items);
            _mailService.AddRowFromTemplate(ExpandoToMailOrder.Map(order,
                items.Where(item => order.items.Any(i => item.ITEM_ID == i.itemId)).ToList(), pohodaId));
        }

        private static void AddMissingItems(List<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            if (!items.Exists(i => i.ITEM_ID == 294489))
                items.Add(
                    new GetPrehomeFeed.SHOPSHOPITEM
                    {
                        ITEM_ID = 294489,
                        PRODUCTNAME = "Intex 69629 Skladacie vesla 218cm",
                        EAN = "6941057417837",
                        IMGURL = "",
                        PRICE_VAT = 15.47m,
                        VAT = 20
                    });

            if (!items.Exists(i => i.ITEM_ID == 261245))
                items.Add(
                    new GetPrehomeFeed.SHOPSHOPITEM
                    {
                        ITEM_ID = 261245,
                        PRODUCTNAME = "Kvetináč Strend Pro Woodeff, 41,5x29x19cm, whiskey barel wagon",
                        EAN = "8584163031795",
                        IMGURL = "",
                        PRICE_VAT = 36.60m,
                        VAT = 20
                    });
            if (!items.Exists(i => i.ITEM_ID == 320751))
                items.Add(
                    new GetPrehomeFeed.SHOPSHOPITEM
                    {
                        ITEM_ID = 320751,
                        PRODUCTNAME = "Doplnkový set obrázkov magic mags zelený traktor k aktovkám grade, space, cloud, 2v1",
                        EAN = "4047443358301",
                        DEALER = "JUNIOR",
                        IMGURL = "",
                        PRICE_VAT = 13.86m,
                        VAT = 20
                    });
            if (!items.Exists(i => i.ITEM_ID == 295379))
                items.Add(
                    new GetPrehomeFeed.SHOPSHOPITEM
                    {
                        ITEM_ID = 295379,
                        PRODUCTNAME = "Úprava Peračník coocazoo pencildenzel, oceanemotion galaxy blue",
                        EAN = "4047443423559",
                        DEALER = "JUNIOR",
                        IMGURL = "",
                        PRICE_VAT = 23.7m,
                        VAT = 20
                    });
            if (!items.Exists(i => i.ITEM_ID == 161412))
                items.Add(
                    new GetPrehomeFeed.SHOPSHOPITEM
                    {
                        ITEM_ID = 161412,
                        PRODUCTNAME = "Rebrík Strend Pro DP 3x06, Alu, EN 131 max. 3.71 m, BASIC",
                        EAN = "5904680990772",
                        DEALER = "-",
                        IMGURL = "",
                        PRICE_VAT = 94.86m,
                        VAT = 20
                    });

        }
    }
}