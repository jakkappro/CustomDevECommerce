using ExpandoFeedTransformer;

namespace PohodaConnector.Builders.Orders;

public interface IGetOrdersByDateRequestBuilder
{
    GetOrdersByDateRequest.dataPack Build();
    GetOrdersByDateRequestBuilder WithDate(short lookBackDays);
}