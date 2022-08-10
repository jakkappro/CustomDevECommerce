using ExpandoConnector.DTO.ExpandoFeed;

namespace ExpandoConnector.Interfaces;

public interface IOrder
{
    Task<GetExpandoFeedRequest.orders> GetOrders(int numberOfDays);
    void UpdateOrder();
}