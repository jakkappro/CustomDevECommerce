using Common.Interfaces;
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
            _logger.LogInformation("Initialized mail service.");

            _mailService.LoadTemplatesFromFile();

            // _orderService.Initialize(_lookBackPohoda);
            // _logger.LogInformation("Initialize order service.");
            //
            // _stock.Initialize();
            // _logger.LogInformation("Initialized stock service.");

            var orders = _expandoService.GetExpandoOrders(_lookBackDays).order;
            _logger.LogInformation("Loaded expando orders.");

            var items = _expandoService.GetPrehomeItems().SHOPITEM.ToList();
            _logger.LogInformation("Loaded prehome orders.");

            foreach (var order in orders.OrderByDescending(o => o.orderStatus).ThenByDescending(o => o.purchaseDate))
            {
                data = new Data();
                if (order.orderStatus != "Unshipped")
                    continue;

                // if (_orderService.Exist(order.orderId))
                // {
                //     _logger.LogInformation("Found order in pohoda, code: {code}", order.orderId);
                // }

                var id = _generator.GetNextId();
                _logger.LogInformation("Id for orderService: {id}", id);

                data.PohodaOrderId = id;
                data.AmazonOrderId = order.orderId;
                data.Status = "Unshipped";

                //CreatePohodaOrder(order, id, items).Wait();

                CreateCarrierPackage(order, id).Wait();

                AddToMail(order, items, id);

                using var db = new LiteDatabase(_configuration["Database:Path"]);
                var col = db.GetCollection<Data>("customers");

                col.Insert(data);
                    
                col.EnsureIndex(x => x.AmazonOrderId);
                col.EnsureIndex(x => x.PohodaOrderId);
                col.EnsureIndex(x => x.InternalPackageId);
            }

            _logger.LogInformation("Sending mail");
            //_mailService.SendMail();
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

                _stock.CreateStock(ExpandoItemToPohodaStock.Map(items.First(e => e.ITEM_ID == stock.itemId)));
                _logger.LogInformation("Created stock, id: {id}", stock.itemId);
            }

            await _orderService.CreateOrder(ExpandoToPohodaOrer.Map(order, id, items));
            _logger.LogInformation("Created order, id: {id}", id);
        }

        private async Task CreateCarrierPackage(GetExpandoFeedRequest.ordersOrder order, string id)
        {
            var packetId = await _carrier.CreatePackage(ExpandoToPacketaPacket.Map(order, id));
            data.InternalPackageId = packetId;
            _logger.LogInformation("Created packet, pohodaId: {id}, packetId: {packetId}", id, packetId);
            Thread.Sleep(1000);
            try
            {
                var res = await _carrier.GetLabel(packetId);
                _logger.LogInformation("Generated label, name: {name}", res);
                _mailService.AddAttachment(res);
                var i = res.Split(".").First();
                using var db = new LiteDatabase(_configuration["Database:Path"]);
                var storage = db.GetStorage<string>();
                storage.Upload(i, $"{_configuration["Packeta:LabelsLocation"]}/{res}");
                data.LabelId = data.PohodaOrderId;
            }
            catch (ArgumentException)
            {
                _logger.LogWarning("Skipping package: {id}", id);
            }

            data.CarrierInfo = "OtherExpressOne";
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
    }
}