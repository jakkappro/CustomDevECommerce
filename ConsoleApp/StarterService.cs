using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class StarterService : IStarterService
{
    private readonly IExpandoOrder _expando;
    private readonly ILogger<StarterService> _logger;

    public StarterService(IExpandoOrder expando, ILogger<StarterService> logger)
    {
        _expando = expando;
        _logger = logger;
    }

    public void Run()
    {
        var res = _expando.GetOrders(1);
        _logger.LogInformation("Amount of orders for day is: {orders}.", res.order.Length);
    }
}