using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using ExpandoConnector.DTO.UpdateFufillment;

namespace ExpandoConnector.Interfaces;

public interface IExpandoOrder
{
    GetExpandoFeedRequest.orders GetExpandoOrders(int numberOfDays);
    void UpdateOrder(UpdateFufillmentRequest data);
    GetPrehomeFeed.SHOP GetPrehomeItems();
}