using Common.Services.Serialization;
using PohodaConnector.Builders.Stock;
using PohodaConnector.DTO.CreateStock;
using PohodaConnector.DTO.GetStock;
using PohodaConnector.Interfaces;

namespace PohodaConnector.Services.StockService;

public class StockService : IStockService
{
    private readonly IAccountingSoftware _mServer;
    private readonly ISerializer _serializer;

    public async Task<StockService> CreateService(short lookBackDays)
    {
        var service = new StockService();
        service._mServer.StartServer();

        if (!await service._mServer.IsConnectionAvailable(3))
            throw new ArgumentNullException("No _mServer :(");

        return service;
    }

    private StockService()
    {
        _mServer = new MServer.MServer("test", "\"C:\\Program Files (x86)\\STORMWARE\\POHODA SK E1\"",
            "http://127.0.0.1:5336", "admin", "acecom", 1000);

        _serializer = new Utf8SerializerService();
    }

    public void CreateStock(StockData stockData)
    {
        // create stock using PohodaConnector.Builders.Stock.StockBuilder
        var stock = new StockBuilder().BuildFromCreteOrderData(stockData);
    }

    public async Task<bool> Exists(string code)
    {
        var filter = new GetStockRequestBuilder().BuildWithCode(code);

        var response = await _mServer.SendRequest(_serializer.Serialize(filter));

        var stock = _serializer.Deserialize<GetStockResponse.responsePack>(response);

        return stock.responsePackItem.listStock.stock is null;
    }
}