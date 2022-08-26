namespace PohodaConnector.Services.StockService;

public record StockData(string ImgUrl, uint Code, string Ean, decimal Price, string Manufacturer,
    string Description, string Name, string ImgFilePath, string RelatedLink, IEnumerable<string>? AlternativeImages = null);