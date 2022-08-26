using PohodaConnector.DTO.CreateStock;
using PohodaConnector.Services.StockService;

namespace PohodaConnector.Builders.Stock;

public interface IStockBuilder
{
    CreateStockRequest.dataPack BuildFromCreteOrderData(StockData createOrderData);
    CreateStockRequest.dataPack Build();
    StockBuilder WithCode(string code);
    StockBuilder WithEan(string ean);
    StockBuilder WithName(string name);
    StockBuilder WithPrice(decimal price);
    StockBuilder WithManufacturer(string manufacturer);
    StockBuilder WithDescription(string description);
    StockBuilder WithPicture(string picture, string picturePath);
    StockBuilder WithRelatedLink(string link);
    StockBuilder WithAlternativePictures(IEnumerable<string> pictures, string picturePath);
}