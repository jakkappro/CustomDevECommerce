using PohodaConnector.DTO.GetStock;

namespace PohodaConnector.Builders.Stock;

public interface IGetStockRequestBuilder
{
    GetStockRequest.dataPack BuildWithCode(string code);
    GetStockRequest.dataPack Build();
    GetStockRequestBuilder WithCode(string code);
}