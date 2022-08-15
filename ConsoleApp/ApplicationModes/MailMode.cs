using Common.Interfaces;
using ConsoleApp.Mappers;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Interfaces;

namespace ConsoleApp.ApplicationModes
{
    public class MailMode : IStarterService
    {
        private readonly int _lookBackDays;
        private readonly IExpandoOrder _expandoService;
        private readonly IMailSender _mailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailMode> _logger;
        private readonly ICarrier _carrier;
        private readonly IIdGenerator _generator;

        public MailMode(int lookBackDays, IExpandoOrder expandoService, IMailSender mailService, IConfiguration configuration, ILogger<MailMode> logger, ICarrier carrier, IIdGenerator generator)
        {
            _lookBackDays = lookBackDays;
            _expandoService = expandoService;
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _carrier = carrier;
            _generator = generator;
        }

        public void Run()
        {
            _logger.LogDebug("Initializing mail service.");
            _mailService.Initialize(_configuration["Email:Login"], _configuration["Email:Recipient"], _configuration["Email:Cc"], _configuration["Email:Password"]);
            _logger.LogDebug("Getting templates for mail.");
            _mailService.LoadTemplatesFromFile();
            var orders = _expandoService.GetExpandoOrders(_lookBackDays).order;
            var items = _expandoService.GetPrehomeItems().SHOPITEM.ToList();

            _logger.LogInformation("Populating mail with orders.");
            foreach (var order in orders.OrderByDescending(o => o.orderStatus).OrderByDescending(o => o.purchaseDate))
            {
                if (order.orderStatus == "Unshipped")
                {
                    AddToMail(order, items);
                    var id = _generator.GetNextId();
                    _logger.LogInformation("Id for order: {id}", id);
                    // create pohoda order
                    //CreateCarrierPackage(order, id);
                }
            }

            _logger.LogInformation("Sending mail");
            _mailService.SendMail();
        }

        private void CreateCarrierPackage(GetExpandoFeedRequest.ordersOrder order, string id)
        {
            _logger.LogInformation("Creating packet: {id}", id);
            _carrier.CreatePackage(ExpandoToPacketaPacket.Map(order, id)).Wait();
        }

        private void AddToMail(GetExpandoFeedRequest.ordersOrder order, IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
        {
            _logger.LogDebug("Adding order id: {orderId}, with items {items} to mail.", order.orderId, order.items);
            _mailService.AddRowFromTemplate(ExpandoToMailOrder.Map(order, items.Where(item => order.items.Any(i => item.ITEM_ID == i.itemId)).ToList()));
        }
    }
}
