using PohodaConnector.DTO.CreateStock;
using PohodaConnector.Services.StockService;

namespace PohodaConnector.Builders.Stock;

public interface IStockBuilder
{
    CreateStockRequest.dataPack BuildFromCreteOrderData(StockData createOrderData);
    CreateStockRequest.dataPack Build();
    StockBuilder WithCode(uint code);
    StockBuilder WithEan(string ean);
    StockBuilder WithName(string name);
    StockBuilder WithPrice(decimal price);
    StockBuilder WithManufacturer(string manufacturer);
    StockBuilder WithDescription(string description);
    StockBuilder WithPicture(string picture);
    StockBuilder WithRelatedLink(string link);
}