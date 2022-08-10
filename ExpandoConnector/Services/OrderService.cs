using Common.Interfaces;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.Interfaces;

namespace ExpandoConnector.Services;

public class OrderService : IOrder
{
    private readonly HttpClient _client;
    private readonly IXmlFeedParser _parser;

    public OrderService(HttpClient client, IXmlFeedParser parser)
    {
        _client = client;
        _parser = parser;
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