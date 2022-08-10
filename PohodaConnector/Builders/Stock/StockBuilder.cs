using PohodaConnector.DTO.CreateOrder;
using PohodaConnector.DTO.CreateStock;
using PohodaConnector.Services.StockService;

namespace PohodaConnector.Builders.Stock;

public class StockBuilder
{
    private CreateStockRequest.dataPack _stock;
    private const decimal Version = 2.0m;
    private const int Ico = 53870441;
    private const string Id = "zas001";
    private const string Note = "Export";
    private const string Application = "StwTest";

    public StockBuilder()
    {
        _stock = new CreateStockRequest.dataPack()
        {
            version = Version,
            ico = Ico,
            note = Note,
            id = Id,
            application = Application,
            dataPackItem = new[]
            {
                new CreateStockRequest.dataPackDataPackItem()
                {
                    id = Id,
                    version = Version,
                    stock = new CreateStockRequest.stock()
                    {
                        version = Version,
                        stockHeader = new CreateStockRequest.stockStockHeader
                        {
                            stockType = "card",
                            PLU = 0,
                            isSales = false,
                            isInternet = true,
                            isBatch = true,
                            purchasingRateVAT = "high",
                            sellingRateVAT = "high",
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
                            limitMin = 0,
                            limitMax = 1000,
                            mass = 0,
                            supplier = new CreateStockRequest.stockStockHeaderSupplier
                            {
                                id = 1
                            },

                            note = "Importovane z xml",
                        }
                    }
                }
            }
        };
    }

    public CreateStockRequest.dataPack BuildFromCreteOrderData(StockData createOrderData)
    {
        return new StockBuilder()
            .WithCode(createOrderData.Code)
            .WithName(createOrderData.Name)
            .WithPrice(createOrderData.Price)
            .WithEan(createOrderData.Ean)
            .WithManufacturer(createOrderData.Manufacturer)
            .WithDescription(createOrderData.Description)
            .WithPicture(createOrderData.Picture)
            .WithRelatedLink(createOrderData.RelatedLink)
            .Build();
    }

    public CreateStockRequest.dataPack Build()
    {
        return _stock;
    }

    public StockBuilder WithCode(uint code)
    {
        _stock.dataPackItem[0].stock.stockHeader.code = code;
        return this;
    }

    public StockBuilder WithEan(string ean)
    {
        _stock.dataPackItem[0].stock.stockHeader.EAN = ean;
        return this;
    }

    public StockBuilder WithName(string name)
    {
        _stock.dataPackItem[0].stock.stockHeader.name = name;
        return this;
    }

    public StockBuilder WithPrice(decimal price)
    {
        _stock.dataPackItem[0].stock.stockHeader.sellingPrice = price;
        return this;
    }

    public StockBuilder WithManufacturer(string manufacturer)
    {
        _stock.dataPackItem[0].stock.stockHeader.producer = manufacturer;
        return this;
    }

    public StockBuilder WithDescription(string description)
    {
        _stock.dataPackItem[0].stock.stockHeader.description = description;
        return this;
    }

    public StockBuilder WithPicture(string picture)
    {
        _stock.dataPackItem[0].stock.stockHeader.pictures = new CreateStockRequest.stockStockHeaderPictures
        {
            picture = new CreateStockRequest.stockStockHeaderPicturesPicture
            {
                filepath = picture,
                @default = true,
                description = "obrazok produktu",
            }
        };
        return this;
    }

    public StockBuilder WithRelatedLink(string link)
    {
        _stock.dataPackItem[0].stock.stockHeader.relatedLinks.relatedLink =
            new CreateStockRequest.stockStockHeaderRelatedLinksRelatedLink
            {
                description = "odkaz na produkt",
                order = 1,
                addressURL = link
            };
        return this;
    }
}