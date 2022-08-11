using ExpandoConnector.DTO.ExpandoFeed;

namespace ExpandoConnector.Interfaces;

public interface IExpandoOrder
{
    GetExpandoFeedRequest.orders GetOrders(int numberOfDays);
    void UpdateOrder();
}