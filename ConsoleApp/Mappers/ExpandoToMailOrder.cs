using System.Globalization;
using Common.Services.MailService;
using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;

namespace ConsoleApp.Mappers;

public static class ExpandoToMailOrder
{
    public static OrderData Map(GetExpandoFeedRequest.ordersOrder data, List<GetPrehomeFeed.SHOPSHOPITEM> items,
        string pohodaId = "")
    {
        return new OrderData
        {
            OrderId = data.orderId,
            PurchaseDate = data.purchaseDate,
            TotalPrice = data.totalPrice.ToString(CultureInfo.InvariantCulture),
            CompanyName = data.customer.companyName is "-" ? "" : data.customer.companyName,
            Name = data.customer.firstname + " " + data.customer.surname,
            Address = data.customer.address.address1,
            City = data.customer.address.city,
            Zip = data.customer.address.zip,
            Country = data.customer.address.country,
            PohodaId = pohodaId,
            Items = items.Select(i => new Item
            {
                ItemId = i.ITEM_ID.ToString(),
                Ean = i.EAN,
                Url = i.URL,
                Dealer = i.DEALER
            }).ToList()
        };
    }
}