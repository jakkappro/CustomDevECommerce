using Common.Interfaces;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.DTO.UpdateFufillment;
using ExpandoConnector.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpandoConnector.Services;

public class ExpandoOrderService : IExpandoOrder
{
    private readonly ILogger<ExpandoOrderService> _logger;
    private readonly HttpClient _client;
    private readonly IXmlFeedParser _parser;

    public ExpandoOrderService(IXmlFeedParser parser, ILogger<ExpandoOrderService> logger, HttpClient client)
    {
        _parser = parser;
        _logger = logger;
        _client = client;
    }

    public GetExpandoFeedRequest.orders GetExpandoOrders(int numberOfDays)
    {
        return _parser.Parse<GetExpandoFeedRequest.orders>(
            $"https://app.expan.do/api/v2/orderfeed?access_token=11w1QgSM7YR4tHyr4BR0BV&days={numberOfDays}");
    }

    public GetPrehomeFeed.SHOP GetPrehomeItems()
    {
        return _parser.Parse<GetPrehomeFeed.SHOP>("https://www.prehome.sk/feed/amazon.xml");
    }

    public void UpdateOrder(UpdateFufillmentRequest data)
    {
        _client.PostAsync("/fulfillment", new StringContent(data.ToJson()));
    }
}