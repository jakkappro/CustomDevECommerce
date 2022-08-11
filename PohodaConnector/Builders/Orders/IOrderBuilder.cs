using PohodaConnector.DTO.CreateOrder;
using PohodaConnector.Services.OrderService;

namespace PohodaConnector.Builders.Orders;

public interface IOrderBuilder
{
    CreateOrderRequest.dataPack BuildFromCreteOrderData(CreateOrderData createOrderData);
    CreateOrderRequest.dataPack Build();
    OrderBuilder AddOrderNumber(string orderId);
    OrderBuilder AddOrderDate(DateTime orderDate);
    OrderBuilder AddOrderText(string orderText);
    OrderBuilder AddName(string name);
    OrderBuilder AddCity(string city);
    OrderBuilder AddStreet(string street);
    OrderBuilder AddZip(string zip);
    OrderBuilder AddCountry(string country);
    OrderBuilder AddCompany(string company);
    OrderBuilder AddEmail(string email);
    OrderBuilder AddPhone(string phone);
    OrderBuilder AddMoss(string moss);
    OrderBuilder AddNumber(string number);
    OrderBuilder AddCarrier(string carrier);
    OrderBuilder SetExecuted(bool value);
    OrderBuilder AddOrderDetailItem(byte quantity, string? ean, decimal price, string? text);
}