namespace PohodaConnector.Services.OrderService;

public class CreateOrderItemData
{
    private CreateOrderItemData(byte quantity, string? ean, decimal price, string? text)
    {
        Quantity = quantity;
        Ean = ean;
        Price = price;
        Text = text;
    }

    public byte Quantity { get; }
    public string? Ean { get; }
    public decimal Price { get; }
    public string? Text { get; }

    public static CreateOrderItemData CreateEanInstance(byte quantity, string? ean, decimal price)
    {
        return new CreateOrderItemData(quantity, ean, price, null);
    }

    public static CreateOrderItemData CreateSimpleInstance(byte quantity, decimal price, string? text)
    {
        return new CreateOrderItemData(quantity, null, price, text);
    }
}