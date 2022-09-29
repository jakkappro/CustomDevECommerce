using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using PohodaConnector.Services.OrderService;

namespace ConsoleApp.Mappers;

public class ExpandoToPohodaOrder
{
    public static CreateOrderData Map(GetExpandoFeedRequest.ordersOrder order, string id,
        IEnumerable<GetPrehomeFeed.SHOPSHOPITEM> items)
    {
        try
        {
            var createOrderItemData = order.items.ToList().Select(e => (items.Contains(items.FirstOrDefault(i => i.ITEM_ID == e.itemId)))
            ?
             CreateOrderItemData.CreateEanInstance(e.itemQuantity, items.FirstOrDefault(i => i.ITEM_ID == e.itemId)?.EAN ?? String.Empty,
                    e.itemPrice)
            :
            CreateOrderItemData.CreateSimpleInstance(e.itemQuantity, e.itemPrice, string.Empty)).ToList();

            createOrderItemData.Add(CreateOrderItemData.CreateSimpleInstance(1, order.shippingPrice, "Doprava"));
            return new CreateOrderData(
                id,
                order.customer.firstname + " " + order.customer.surname,
                order.customer.address.city,
                order.customer.address.address1 + " " + order.customer.address.address2 ?? "" + " " + order.customer.address.address3 ?? "",
                order.customer.address.zip,
                order.customer.address.country,
                order.customer.companyName is "-" ? "" : order.customer.companyName,
                order.customer.email,
                DateTime.Parse(order.purchaseDate),
                order.customer.phone,
                order.orderId,
                order.customer.address.country,
                order.customer.address.country switch
                {
                    "DE" => "Doprava DE",
                    "AT" => "Doprava AT",
                    "FR" => "Doprava FR",
                    _ => throw new ArgumentException()
                },
                createOrderItemData
            );
        }
        catch
        {
            return null;
        }
    }
}