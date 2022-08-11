using Common.Interfaces;
using Microsoft.Extensions.Logging;
using PohodaConnector.Builders.Orders;
using PohodaConnector.DTO.GetOrdersByDate;
using PohodaConnector.Interfaces;

namespace PohodaConnector.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IAccountingSoftware _server;
    private List<GetOrdersByDateResponse.listOrderOrder> _existingOrders;
    private readonly ILogger<OrderService> _logger;
    private readonly ISerializer _serializer;

    public OrderService(ILogger<OrderService> logger, ISerializer serializer, IAccountingSoftware server)
    {
        _logger = logger;
        _serializer = serializer;
        _server = server;
    }

    public async void Initialize(short lookBackDays)
    {
        _server.Initialize("test", "\"C:\\Program Files (x86)\\STORMWARE\\POHODA SK E1\"",
            "http://127.0.0.1:5336", "admin", "acecom", 1000);

        _server.StartServer();

        if (!await _server.IsConnectionAvailable(3))
        {
            _logger.LogError("Can't connect to server.");
            throw new ArgumentNullException();
        }

        var getOrdersByDateRequest = new GetOrdersByDateRequestBuilder()
            .WithDate(lookBackDays)
            .Build();

        _existingOrders =
            (_serializer.Deserialize<GetOrdersByDateResponse.responsePack>(
                    await _server.SendRequest(_serializer.Serialize(getOrdersByDateRequest)))
                .responsePackItem.listOrder.order ?? Array.Empty<GetOrdersByDateResponse.listOrderOrder>()).ToList();
    }

    public async void CreateOrderAsync(CreateOrderData createOrderData)
    {
        var order = new OrderBuilder().BuildFromCreteOrderData(createOrderData);

        var str = _serializer.Serialize(order);

        await _server.SendRequest(str);
    }

    public bool Exist(string id)
    {
        return _existingOrders.Any(x => x.orderHeader.numberOrder == id);
    }

    public async void UpdateOrder(string id, bool executed = true)
    {
        throw new NotImplementedException();
    }
}