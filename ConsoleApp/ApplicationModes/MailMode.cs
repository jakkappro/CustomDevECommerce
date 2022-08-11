using Common.Interfaces;
using ConsoleApp.Mappers;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.ApplicationModes
{
    public class MailMode : IStarterService
    {
        private readonly int _lookBackDays;
        private readonly IExpandoOrder _expandoService;
        private readonly IMailSender _mailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailMode> _logger;

        public MailMode(int lookBackDays, IExpandoOrder expandoService, IMailSender mailService, IConfiguration configuration, ILogger<MailMode> logger)
        {
            _lookBackDays = lookBackDays;
            _expandoService = expandoService;
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
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
            foreach (var order in orders)
            {
                _logger.LogDebug("Adding order id: {orderId}, with items {items}", order.orderId, order.items);
                _mailService.AddRowFromTemplate(ExpandoToMailOrder.Map(order, items.Where(item => order.items.Any(i => item.ITEM_ID == i.itemId)).ToList()));
            }

            _logger.LogInformation("Sending mail");
            _mailService.SendMail();
        }
    }
}
