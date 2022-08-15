namespace PohodaConnector.Services.StockService;

public class StockData
{
    public StockData(string imgUrl, uint code, string ean,decimal price, string manufacturer,
        string description, string name, string imgFilePath, string relatedLink)
    {
        ImgUrl = imgUrl;
        Code = code;
        Ean = ean;
        Price = price;
        Manufacturer = manufacturer;
        Description = description;
        Name = name;
        ImgFilePath = imgFilePath;
        RelatedLink = relatedLink;
    }

    public string ImgUrl { get; }
    public uint Code { get; }
    public string Ean { get; }
    public decimal Price { get; }
    public string Manufacturer { get; }
    public string Description { get; }
    public string Name { get; }
    public string ImgFilePath { get; }
    public string RelatedLink { get; }
}