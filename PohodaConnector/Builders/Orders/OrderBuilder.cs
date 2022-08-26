using PohodaConnector.DTO.CreateOrder;
using PohodaConnector.Services.OrderService;

namespace PohodaConnector.Builders.Orders;

public class OrderBuilder : IOrderBuilder
{
    private const decimal Version = 2.0m;
    private const int Ico = 53870441;
    private const string Id = "zas001";
    private const string PaymentType = "Plat.kartou";
    private const string RoundingDocument = "none";
    private const string OrderType = "receivedOrder";
    private const string Note = "Export";
    private const string Application = "StwTest";
    private const string EvidentiaryResourcesMoss = "A";
    private const string TypeServiceMoss = "GD";
    private const string OrderText = "Amazon objednavka c. ";
    private readonly List<CreateOrderRequest.orderOrderItem> _items;
    private readonly CreateOrderRequest.dataPack _order;

    public OrderBuilder()
    {
        _items = new List<CreateOrderRequest.orderOrderItem>();
        _order = new CreateOrderRequest.dataPack
        {
            version = Version,
            ico = Ico,
            note = Note,
            id = Id,
            application = Application,
            dataPackItem = new CreateOrderRequest.dataPackDataPackItem
            {
                id = Id,
                version = Version,
                order = new CreateOrderRequest.order
                {
                    version = Version,
                    orderHeader = new CreateOrderRequest.orderOrderHeader
                    {
                        orderType = OrderType,
                        paymentType = new CreateOrderRequest.orderOrderHeaderPaymentType
                        {
                            ids = PaymentType
                        },
                        evidentiaryResourcesMOSS =
                            new CreateOrderRequest.orderOrderHeaderEvidentiaryResourcesMOSS
                            {
                                ids = EvidentiaryResourcesMoss
                            },
                        isDelivered = false,
                        partnerIdentity = new CreateOrderRequest.orderOrderHeaderPartnerIdentity
                        {
                            address = new CreateOrderRequest.address
                            {
                                country = new CreateOrderRequest.addressCountry()
                            }
                        },
                        MOSS = new CreateOrderRequest.orderOrderHeaderMOSS(),
                        carrier = new CreateOrderRequest.orderOrderHeaderCarrier(),
                        number = new CreateOrderRequest.orderOrderHeaderNumber()
                    },
                    orderSummary = new CreateOrderRequest.orderOrderSummary
                    {
                        roundingDocument = RoundingDocument
                    }
                }
            }
        };
    }

    public CreateOrderRequest.dataPack BuildFromCreteOrderData(CreateOrderData createOrderData)
    {
        var order = new OrderBuilder()
            .AddOrderNumber(createOrderData.OrderNumber)
            .AddOrderDate(createOrderData.PurchaseDate)
            .AddOrderText(OrderText + createOrderData.OrderNumber)
            .AddName(createOrderData.Fullname)
            .AddCity(createOrderData.City)
            .AddStreet(createOrderData.Address)
            .AddZip(createOrderData.Zip)
            .AddCountry(createOrderData.Country)
            .AddCompany(createOrderData.CompanyName)
            .AddEmail(createOrderData.Email)
            .AddPhone(createOrderData.Phone)
            .AddMoss(createOrderData.Moss)
            .AddNumber(createOrderData.OrderId)
            .AddCarrier(createOrderData.Carrier)
            .SetExecuted(false);

        foreach (var data in createOrderData.OrderItem)
            order.AddOrderDetailItem(data.Quantity, data.Ean, data.Price, data.Text);

        return order.Build();
    }

    public CreateOrderRequest.dataPack Build()
    {
        var ord = _order;
        ord.dataPackItem.order.orderDetail = _items.ToArray();
        _items.Clear();
        return ord;
    }

    public OrderBuilder AddOrderNumber(string orderId)
    {
        _order.dataPackItem.order.orderHeader.numberOrder = orderId;
        return this;
    }

    public OrderBuilder AddOrderDate(DateTime orderDate)
    {
        _order.dataPackItem.order.orderHeader.date = orderDate;
        _order.dataPackItem.order.orderHeader.dateFrom = orderDate;
        _order.dataPackItem.order.orderHeader.dateTo = orderDate;
        return this;
    }

    public OrderBuilder AddOrderText(string orderText)
    {
        _order.dataPackItem.order.orderHeader.text = orderText;
        return this;
    }

    public OrderBuilder AddName(string name)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.name = name;
        return this;
    }

    public OrderBuilder AddCity(string city)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.city = city;
        return this;
    }

    public OrderBuilder AddStreet(string street)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.street = street;
        return this;
    }

    public OrderBuilder AddZip(string zip)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.zip = zip;
        return this;
    }

    public OrderBuilder AddCountry(string country)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.country.ids = country;
        return this;
    }

    public OrderBuilder AddCompany(string company)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.company = company;
        return this;
    }

    public OrderBuilder AddEmail(string email)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.email = email;
        return this;
    }

    public OrderBuilder AddPhone(string phone)
    {
        _order.dataPackItem.order.orderHeader.partnerIdentity.address.mobilPhone = phone;
        return this;
    }

    public OrderBuilder AddMoss(string moss)
    {
        _order.dataPackItem.order.orderHeader.MOSS.ids = moss;
        return this;
    }

    public OrderBuilder AddNumber(string number)
    {
        _order.dataPackItem.order.orderHeader.number.numberRequested = number;
        return this;
    }

    public OrderBuilder AddCarrier(string carrier)
    {
        _order.dataPackItem.order.orderHeader.carrier.ids = carrier;
        return this;
    }

    public OrderBuilder SetExecuted(bool value)
    {
        _order.dataPackItem.order.orderHeader.isExecuted = value;
        return this;
    }

    public OrderBuilder AddOrderDetailItem(byte quantity, string? ean, decimal price, string? text)
    {
        var item = new CreateOrderRequest.orderOrderItem
        {
            text = text,
            quantity = quantity,
            delivered = 0,
            rateVAT = "historyHigh",
            homeCurrency = new CreateOrderRequest.orderOrderItemHomeCurrency
            {
                unitPrice = price
            },
            stockItem = new CreateOrderRequest.orderOrderItemStockItem
            {
                stockItem = new CreateOrderRequest.stockItem
                {
                    EAN = ean
                }
            },
            typeServiceMOSS = new CreateOrderRequest.orderOrderItemTypeServiceMOSS
            {
                ids = TypeServiceMoss
            },
            payVAT = true
        };

        _items.Add(item);
        return this;
    }
}