using PohodaConnector.Services.OrderService;

namespace PohodaConnector.Interfaces;

public interface IOrderService
{
    void CreateOrderAsync(CreateOrderData createOrderData);

    bool Exist(string id);

    void UpdateOrder(string id, bool executed = false);
}