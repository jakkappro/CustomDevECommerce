using Common.Interfaces;
using ConsoleApp.Mappers;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.Interfaces;
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
        private readonly IOrderService _order;
        private readonly IStockService _stock;


        public MailMode(int lookBackDays, IExpandoOrder expandoService, IMailSender mailService, IConfiguration configuration, ILogger<MailMode> logger, ICarrier carrier, IIdGenerator generator, IOrderService order, IStockService stock)
        {
            _lookBackDays = lookBackDays;
            _expandoService = expandoService;
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _carrier = carrier;
            _generator = generator;
            _order = order;
            _stock = stock;
        }

        public void Run()
        {
            _logger.LogDebug("Initializing mail service.");
            _mailService.Initialize(_configuration["Email:Login"], _configuration["Email:Recipient"], _configuration["Email:Cc"], _configuration["Email:Password"]);
            _logger.LogDebug("Getting templates for mail.");
            _mailService.LoadTemplatesFromFile();

            _order.Initialize(_lookBackPohoda);
            _stock.Initialize();

            var orders = _expandoService.GetExpandoOrders(_lookBackDays).order;
            var items = _expandoService.GetPrehomeItems().SHOPITEM.ToList();

            _logger.LogInformation("Populating mail with orders.");
            foreach (var order in orders.OrderByDescending(o => o.orderStatus).ThenByDescending(o => o.purchaseDate))
            {
                if (order.orderStatus != "Unshipped" || _order.Exist(order.orderId)) 
                    continue;
                
                var id = _generator.GetNextId();
                
                AddToMail(order, items, id);
                _logger.LogInformation("Id for order: {id}", id);

                CreatePohodaOrder(order, id, items).Wait();

                CreateCarrierPackage(order, id).Wait();
            }

            _logger.LogInformation("Sending mail");
            _mailService.SendMail();
        }

        private async Task CreatePohodaOrder(GetExpandoFeedRequest.ordersOrder order, string id, IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            foreach (var stock in order.items)
            {
                if (await _stock.Exists(stock.itemId.ToString()))
                {
                    continue;
                }

                _stock.CreateStock(ExpandoItemToPohodaStock.Map(items.First(e => e.ITEM_ID == stock.itemId)));
            }
            await _order.CreateOrder(ExpandoToPohodaOrer.Map(order, id, items));
        }

        private async Task CreateCarrierPackage(GetExpandoFeedRequest.ordersOrder order, string id)
        {
            _logger.LogInformation("Creating packet: {id}", id);
            var packetId = await _carrier.CreatePackage(ExpandoToPacketaPacket.Map(order, id));
            Thread.Sleep(1000);
            try
            {
                var res = await _carrier.GetLabel(packetId);
                _mailService.AddAttachment(res);
            }
            catch (ArgumentException)
            {
                _logger.LogWarning("Skipping package: {id}", id);
            }
        }

        private void AddToMail(GetExpandoFeedRequest.ordersOrder order, IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items, string pohodaId)
        {
            _logger.LogDebug("Adding order id: {orderId}, with items {items} to mail.", order.orderId, order.items);
            _mailService.AddRowFromTemplate(ExpandoToMailOrder.Map(order, items.Where(item => order.items.Any(i => item.ITEM_ID == i.itemId)).ToList(), pohodaId));
        }
    }
}
