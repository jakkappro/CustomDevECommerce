using Common.Interfaces;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpandoConnector.Services;

public class ExpandoOrderService : IOrder
{
    private readonly HttpClient _client;
    private readonly IXmlFeedParser _parser;
    private readonly ILogger<ExpandoOrderService> _logger;

    public ExpandoOrderService(HttpClient client, IXmlFeedParser parser, ILogger<ExpandoOrderService> logger)
    {
        _client = client;
        _parser = parser;
        _logger = logger;
    }

    public async Task<GetExpandoFeedRequest.orders> GetOrders(int numberOfDays)
    {
        return await _parser.Parse<GetExpandoFeedRequest.orders>($"https://app.expan.do/api/v2/orderfeed?access_token=11w1QgSM7YR4tHyr4BR0BV&days={numberOfDays}");
    }

    public void UpdateOrder()
    {
        throw new NotImplementedException();
    }
}