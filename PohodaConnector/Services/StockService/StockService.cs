﻿using Common.Interfaces;
using Microsoft.Extensions.Logging;
using PohodaConnector.Builders.Stock;
using PohodaConnector.DTO.GetStock;
using PohodaConnector.Interfaces;

namespace PohodaConnector.Services.StockService;

public class StockService : IStockService
{
    private readonly ILogger<StockService> _logger;
    private readonly IAccountingSoftware _server;
    private readonly IDownloader _imageDownloader;
    private readonly ISerializer _serializer;

    public StockService(ISerializer serializer, ILogger<StockService> logger, IAccountingSoftware server, IDownloader imageDownloader)
    {
        _serializer = serializer;
        _logger = logger;
        _server = server;
        _imageDownloader = imageDownloader;
    }

    public async Task Initialize()
    {
        _server.Initialize("test", "\"C:\\Program Files (x86)\\STORMWARE\\POHODA SK E1\"", 1000);
        _server.StartServer();

        if (await _server.IsConnectionAvailable(3))
            return;

        _logger.LogError("Error, can't connect to accounting software.");
        throw new ArgumentNullException("No _server :(");

    }

    public async void CreateStock(StockData stockData)
    {
        var imageFileName = stockData.ImgUrl.Split("/").Last();
        var path = $"{stockData.ImgFilePath}{imageFileName}";

        _logger.LogDebug("Downloading picture with url {url} and destination {destination}. StockData ImgFilePath is {ImgFilePath}", stockData.ImgUrl, path, stockData.ImgFilePath);
        _imageDownloader.Download(stockData.ImgUrl, path).Wait();
        _logger.LogInformation("Downloaded image, path: {path}", path);
        
        var stock = new StockBuilder().BuildFromCreteOrderData(stockData);
        var serialize = _serializer.Serialize(stock);
        _logger.LogDebug("Creating stock: {stock}", stock);
        await _server.SendRequest(serialize);
    }

    public async Task<bool> Exists(string code)
    {
        var filter = new GetStockRequestBuilder().BuildWithCode(code);

        var response = await _server.SendRequest(_serializer.Serialize(filter));

        var stock = _serializer.Deserialize<GetStockResponse.responsePack>(response);

        return stock.responsePackItem.listStock.stock is not null;
    }
}