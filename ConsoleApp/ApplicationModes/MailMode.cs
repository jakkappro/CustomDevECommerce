﻿using Common.Interfaces;
using ConsoleApp.Mappers;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Interfaces;
using PohodaConnector.Interfaces;
using System.Text.Json;

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


        public MailMode(int lookBackDays, IExpandoOrder expandoService, IMailSender mailService,
            IConfiguration configuration, ILogger<MailMode> logger, ICarrier carrier, IIdGenerator generator,
            IOrderService orderService, IStockService stock)
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
        }

        public void Run()
        {
            _mailService.Initialize(_configuration["Email:Login"], _configuration["Email:Recipient"],
                _configuration["Email:Cc"], _configuration["Email:Password"]);
            _logger.LogInformation("Mail service initialized.");

            _mailService.LoadTemplatesFromFile();

            _orderService.Initialize(_lookBackPohoda);
            _logger.LogInformation("Order service initialized.");

            _stock.Initialize();
            _logger.LogInformation("Stock service initialized.");

            var orders = _expandoService.GetExpandoOrders(_lookBackDays).order;
            _logger.LogInformation("Expando orders sucessfully loaded.");

            var items = _expandoService.GetPrehomeItems().SHOPITEM.ToList();
            _logger.LogInformation("Prehome orders sucessfully loaded.");

            foreach (var order in orders.OrderByDescending(o => o.orderStatus).ThenByDescending(o => o.purchaseDate))
            {
                if (order.orderStatus != "Unshipped")
                    continue;

                if (_orderService.Exist(order.orderId))
                {
                    _logger.LogInformation("Found order in pohoda, code: {code}", order.orderId);
                    continue;
                }

                var id = _generator.GetNextId();
                _logger.LogInformation($"Id of new order: {id}");

                CreatePohodaOrder(order, id, items).Wait();

                CreateCarrierPackage(order, id).Wait();

                AddToMail(order, items, id);
            }

            _logger.LogInformation("Sending mail");
            _mailService.SendMail();
        }

        private async Task CreatePohodaOrder(GetExpandoFeedRequest.ordersOrder order, string id,
            IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            foreach (var stock in order.items)
            {
                if (await _stock.Exists(stock.itemId.ToString()))
                {
                    _logger.LogInformation("Found stock, id: {id}", stock.itemId);
                    continue;
                }

                _logger.LogDebug($"Creating stock...{JsonSerializer.Serialize(stock)}");
                _stock.CreateStock(ExpandoItemToPohodaStock.Map(items.First(e => e.ITEM_ID == stock.itemId)));
                _logger.LogInformation($"Stock with id {stock.itemId} sucessfully created");
            }

            _logger.LogDebug($"Creating order.. {JsonSerializer.Serialize(order)}");
            await _orderService.CreateOrder(ExpandoToPohodaOrer.Map(order, id, items));
            _logger.LogInformation($"Order with {id} sucessfully created");
        }

        private async Task CreateCarrierPackage(GetExpandoFeedRequest.ordersOrder order, string id)
        {
            var packetData = ExpandoToPacketaPacket.Map(order, id);
            _logger.LogDebug($"Creating packet...{JsonSerializer.Serialize(packetData)}");
            var packetId = await _carrier.CreatePackage(packetData);
            _logger.LogInformation("Created packet, pohodaId: {id}, packetId: {packetId}", id, packetId);
            Thread.Sleep(1000);
            try
            {
                var res = await _carrier.GetLabel(packetId);
                _logger.LogInformation("Generated label, name: {name}", res);
                _mailService.AddAttachment(res);
            }
            catch (ArgumentException)
            {
                _logger.LogWarning("Skipping package: {id}", id);
            }
        }

        private void AddToMail(GetExpandoFeedRequest.ordersOrder order, IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items,
            string pohodaId)
        {
            _logger.LogDebug("Adding orderService id: {orderId}, with items {items} to mail.", order.orderId,
                order.items);
            _mailService.AddRowFromTemplate(ExpandoToMailOrder.Map(order,
                items.Where(item => order.items.Any(i => item.ITEM_ID == i.itemId)).ToList(), pohodaId));
        }
    }
}