namespace Common.Services.MailService;

public class Order
{
    public Order(string orderId, string purchaseDate, string totalPrice, string companyName, string name,
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

    public string OrderId { get; }
    public string PurchaseDate { get; }
    public string TotalPrice { get; }
    public string CompanyName { get; }
    public string Name { get; }
    public string Address { get; }
    public string City { get; }
    public string Zip { get; }
    public string Country { get; }
    public string PohodaId { get; }
    public List<Item> Items { get; }
}