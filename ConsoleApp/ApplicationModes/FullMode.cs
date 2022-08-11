using Common.Interfaces;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.ApplicationModes;

public class FullMode : IStarterService
{
    private readonly IExpandoOrder _expando;
    private readonly ILogger<FullMode> _logger;
    private readonly IMailSender _mailSender;
    private readonly int _lookBackDays;

    public FullMode(IExpandoOrder expando, ILogger<FullMode> logger, IMailSender mailSender, int lookBackDays)
    {
        _expando = expando;
        _logger = logger;
        _mailSender = mailSender;
        _lookBackDays = lookBackDays;
    }

    public void Run()
    {
        var res = _expando.GetExpandoOrders(_lookBackDays);
        _logger.LogInformation("Amount of orders for day is: {orders}.", res.order.Length);
    }
}