using PohodaConnector.Services.StockService;

namespace PohodaConnector.Interfaces;

public interface IStockService
{
    void CreateStock(StockData stockData);
    Task<bool> Exists(string code);

    Task Initialize();
}