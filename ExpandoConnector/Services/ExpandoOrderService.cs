using Common.Interfaces;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpandoConnector.Services;

public class ExpandoOrderService : IExpandoOrder
{
    private readonly IXmlFeedParser _parser;
    private readonly ILogger<ExpandoOrderService> _logger;

    public ExpandoOrderService(IXmlFeedParser parser, ILogger<ExpandoOrderService> logger)
    {
        _parser = parser;
        _logger = logger;
    }

    public GetExpandoFeedRequest.orders GetOrders(int numberOfDays)
    {
        return _parser.Parse<GetExpandoFeedRequest.orders>($"https://app.expan.do/api/v2/orderfeed?access_token=11w1QgSM7YR4tHyr4BR0BV&days={numberOfDays}");
    }

    public void UpdateOrder()
    {
        throw new NotImplementedException();
    }
}