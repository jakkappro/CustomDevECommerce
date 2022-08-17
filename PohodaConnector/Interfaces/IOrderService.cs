using PohodaConnector.Services.OrderService;

namespace PohodaConnector.Interfaces;

public interface IOrderService
{
    Task CreateOrder(CreateOrderData createOrderData);

    bool Exist(string id);

    void UpdateOrder(string id, bool executed = false);

    Task Initialize(short lookBackDays);
}