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

    // Prepared for tomorrow :D
    // Btw do we want to download images in builder or should it be another service?
    public async void CreateStock(StockData stockData)
    {
        var dataPack = new CreateStockRequest.dataPack
        {
            version = 2.0m,
            ico = 53870441,
            note = "Imported from xml",
            id = "zas001",
            application = "StwTest"
        };

        var pathToPicture = stockData.ImgUrl.Split('/').Last();
        // Console.WriteLine($"Downloading picture{pathToPicture}");
        // try
        // {
        //     var fileBytes = await client.GetByteArrayAsync(new Uri(i.IMGURL));
        //     if (!File.Exists(path + pathToPicture))
        //     {
        //         await using var fs = File.Create(path + pathToPicture);
        //         await fs.WriteAsync(fileBytes);
        //         fs.Close();
        //     }
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(
        //         $"Failed to download image for {i.PRODUCTNAME}, path {path + pathToPicture}, message {e.Message}");
        // }

        var dataPackItem = new CreateStockRequest.dataPackDataPackItem
        {
            version = 2.0m,
            id = "ZAS001",
            stock = new CreateStockRequest.stock
            {
                version = 2.0m,
                stockHeader = new CreateStockRequest.stockStockHeader
                {
                    stockType = "card",
                    code = stockData.Code,
                    EAN = stockData.Ean,
                    PLU = 0,
                    isSales = false,
                    isInternet = true,
                    isBatch = true,
                    purchasingRateVAT = "high",
                    sellingRateVAT = "high",
                    name = stockData.ProductName,
                    unit = "ks",
                    storage = new CreateStockRequest.stockStockHeaderStorage
                    {
                        ids = "Amazon"
                    },
                    typePrice = new CreateStockRequest.stockStockHeaderTypePrice
                    {
                        ids = "SK"
                    },
                    purchasingPrice = 0,
                    sellingPrice = stockData.Price,
                    limitMin = 0,
                    limitMax = 1000,
                    mass = 0,
                    supplier = new CreateStockRequest.stockStockHeaderSupplier
                    {
                        id = 1
                    },
                    producer = stockData.Manufacturer,
                    description = stockData.Description,
                    pictures = new CreateStockRequest.stockStockHeaderPictures
                    {
                        picture = new CreateStockRequest.stockStockHeaderPicturesPicture
                        {
                            @default = true,
                            description = "obrazok produktu",
                            filepath = pathToPicture
                        }
                    },
                    note = "Importovane z xml",
                    relatedLinks = new CreateStockRequest.stockStockHeaderRelatedLinks
                    {
                        relatedLink = new CreateStockRequest.stockStockHeaderRelatedLinksRelatedLink
                        {
                            addressURL = stockData.Url,
                            description = "odkaz na produkt",
                            order = 1
                        }
                    }
                }
            }
        };

        dataPack.dataPackItem = new[]
        {
            dataPackItem
        };
    }

    public async Task<bool> Exists(string code)
    {
        var filter = new GetStockRequestBuilder().BuildWithCode(code);

        var response = await _mServer.SendRequest(_serializer.Serialize(filter));

        var stock = _serializer.Deserialize<GetStockResponse.responsePack>(response);

        return stock.responsePackItem.listStock.stock is null;
    }
}