using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;

namespace ExpandoConnector.Interfaces;

public interface IExpandoOrder
{
    GetExpandoFeedRequest.orders GetExpandoOrders(int numberOfDays);
    void UpdateOrder();
    GetPrehomeFeed.SHOP GetPrehomeItems();
}