namespace PohodaConnector.Services.OrderService;

public class CreateOrderData
{
    public CreateOrderData(string orderId, string fullname, string city, string address, string zip,
        string country, string companyName, string email, DateTime purchaseDate, string phone, string orderNumber,
        string moss, string carrier, List<CreateOrderItemData> orderItem)
    {
        OrderId = orderId;
        Fullname = fullname;
        City = city;
        Address = address;
        Zip = zip;
        Country = country;
        CompanyName = companyName;
        Email = email;
        PurchaseDate = purchaseDate;
        Phone = phone;
        OrderNumber = orderNumber;
        Moss = moss;
        Carrier = carrier;
        OrderItem = orderItem;
    }

    public string OrderId { get; }
    public string Fullname { get; }
    public string City { get; }
    public string Address { get; }
    public string Zip { get; }
    public string Country { get; }
    public string CompanyName { get; }
    public string Email { get; }
    public DateTime PurchaseDate { get; }
    public string Phone { get; }
    public string OrderNumber { get; }
    public string Moss { get; }
    public string Carrier { get; }
    public List<CreateOrderItemData> OrderItem { get; }
}