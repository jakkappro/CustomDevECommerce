using ExpandoFeedTransformer;

namespace PohodaConnector.Builders.CreateOrder;

public class GetOrdersByDateRequestBuilder
{
    private const decimal Version = 2.0m;
    private const int Ico = 53870441;
    private const string OrderType = "receivedOrder";
    private const string Id = "1";
    private const string Application = "SWTTest";
    private const string Note = "Export objednavok";
    private readonly GetOrdersByDateRequest.dataPack _order;

    public GetOrdersByDateRequestBuilder()
    {
        _order = new GetOrdersByDateRequest.dataPack
        {
            dataPackItem = new GetOrdersByDateRequest.dataPackDataPackItem
            {
                listOrderRequest = new GetOrdersByDateRequest.listOrderRequest
                {
                    requestOrder = new GetOrdersByDateRequest.listOrderRequestRequestOrder
                    {
                        filter = new GetOrdersByDateRequest.filter()
                    },
                    version = Version,
                    orderType = OrderType,
                    orderVersion = Version
                },
                id = Id,
                version = Version
            },
            id = Id,
            ico = Ico,
            application = Application,
            version = Version,
            note = Note
        };
    }

    public GetOrdersByDateRequest.dataPack Build()
    {
        return _order;
    }

    public GetOrdersByDateRequestBuilder WithDate(short lookBackDays)
    {
        _order.dataPackItem.listOrderRequest.requestOrder.filter.dateFrom = (DateTime.Today - TimeSpan.FromDays(lookBackDays)).ToString("yyyy-MM-dd");
        _order.dataPackItem.listOrderRequest.requestOrder.filter.dateTill = DateTime.Now.ToString("yyyy-MM-dd");
        return this;
    }
}