﻿namespace PohodaConnector.Services.StockService;

public class StockData
{
    public StockData(string imgUrl, uint code, string ean, string productName, decimal price, string manufacturer, string description, string url)
    {
        ImgUrl = imgUrl;
        Code = code;
        Ean = ean;
        ProductName = productName;
        Price = price;
        Manufacturer = manufacturer;
        Description = description;
        Url = url;
    }

    public string ImgUrl { get; private set; }
    public uint Code { get; private set; }
    public string Ean { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public string Manufacturer { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
}