using PohodaConnector.DTO.GetStock;

namespace PohodaConnector.Builders.Stock;

public class GetStockRequestBuilder
{
    private const decimal Version = 2.0m;
    private const int Ico = 53870441;
    private const string OrderType = "receivedOrder";
    private const string Id = "1";
    private const string Application = "SWTTest";
    private const string Note = "Export objednavok";
    private readonly GetStockRequest.dataPack _stock;

    public GetStockRequestBuilder()
    {
        _stock = new GetStockRequest.dataPack
        {
            version = Version,
            ico = Ico,
            note = Note,
            id = Id,
            application = Application,
            dataPackItem = new GetStockRequest.dataPackDataPackItem
            {
                id = Id,
                version = Version,
                listStockRequest = new GetStockRequest.listStockRequest
                {
                    version = Version,
                    stockVersion = Version,
                    requestStock = new GetStockRequest.listStockRequestRequestStock
                    {
                        filter = new GetStockRequest.filter()
                    }
                }
            }
        };
    }

    public GetStockRequest.dataPack BuildWithCode(string code)
    {
        var stock = new GetStockRequestBuilder().WithCode(code).Build();
        return stock;
    }

    public GetStockRequest.dataPack Build()
    {
        return _stock;
    }

    public GetStockRequestBuilder WithCode(string code)
    {
        _stock.dataPackItem.listStockRequest.requestStock.filter.code = code;
        return this;
    }
}