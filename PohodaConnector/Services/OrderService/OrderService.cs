using PohodaConnector.Builders.CreateOrder;
using PohodaConnector.Interfaces;
using Common.Services.Serialization;
using PohodaConnector.Builders.Orders;
using PohodaConnector.DTO.GetOrdersByDate;


namespace PohodaConnector.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IAccountingSoftware _mServer;
    private List<GetOrdersByDateResponse.listOrderOrder> _existingOrders;
    private readonly ISerializer _serializer;

    public async Task<OrderService> CreateService(short lookBackDays)
    {
        var service = new OrderService();
        service._mServer.StartServer();

        if (!await service._mServer.IsConnectionAvailable(3))
            throw new ArgumentNullException("No _mServer :(");

        var getOrdersByDateRequest = new GetOrdersByDateRequestBuilder()
            .WithDate(lookBackDays)
            .Build();

        _existingOrders =
            (_serializer.Deserialize<GetOrdersByDateResponse.responsePack>(
                    await service._mServer.SendRequest(_serializer.Serialize(getOrdersByDateRequest)))
                .responsePackItem.listOrder.order ?? Array.Empty<GetOrdersByDateResponse.listOrderOrder>()).ToList();
        
        return service;
    }

    private OrderService()
    {
        _mServer = new MServer.MServer("test", "\"C:\\Program Files (x86)\\STORMWARE\\POHODA SK E1\"",
            "http://127.0.0.1:5336", "admin", "acecom", 1000);
        
        _serializer = new Utf8SerializerService();
    }

    public async void CreateOrderAsync(CreateOrderData createOrderData)
    {
        var order = new OrderBuilder().BuildFromCreteOrderData(createOrderData);

        var str = _serializer.Serialize(order);

        await _mServer.SendRequest(str);
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