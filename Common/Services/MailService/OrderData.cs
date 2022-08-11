#pragma warning disable CS8618
namespace Common.Services.MailService;

public class OrderData
{
    public OrderData(string orderId, string purchaseDate, string totalPrice, string? companyName, string name,
        string address, string city, string zip, string country, string pohodaId, List<Item> items)
    {
        OrderId = orderId;
        PurchaseDate = purchaseDate;
        TotalPrice = totalPrice;
        CompanyName = companyName;
        Name = name;
        Address = address;
        City = city;
        Zip = zip;
        Country = country;
        PohodaId = pohodaId;
        Items = items;
    }

    public OrderData()
    {
    }

    public string OrderId { get; set; }
    public string PurchaseDate { get; set; }
    public string TotalPrice { get; set; }
    public string? CompanyName { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public string PohodaId { get; set; }
    public List<Item> Items { get; set; }
}