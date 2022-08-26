using PohodaConnector.DTO.CreateStock;
using PohodaConnector.Services.StockService;

namespace PohodaConnector.Builders.Stock;

public class StockBuilder : IStockBuilder
{
    private const decimal Version = 2.0m;
    private const int Ico = 53870441;
    private const string Id = "zas001";
    private const string Note = "Export";
    private const string Application = "StwTest";
    private readonly CreateStockRequest.dataPack _stock;

    public StockBuilder()
    {
        _stock = new CreateStockRequest.dataPack
        {
            version = Version,
            ico = Ico,
            note = Note,
            id = Id,
            application = Application,
            dataPackItem = new[]
            {
                new CreateStockRequest.dataPackDataPackItem
                {
                    id = Id,
                    version = Version,
                    stock = new CreateStockRequest.stock
                    {
                        version = Version,
                        stockHeader = new CreateStockRequest.stockStockHeader
                        {
                            stockType = "card",
                            PLU = 0,
                            isSales = true,
                            isInternet = false,
                            isBatch = true,
                            purchasingRateVAT = "high",
                            sellingRateVAT = "high",
                            unit = "ks",
                            storage = new CreateStockRequest.stockStockHeaderStorage
                            {
                                ids = "Hrackys"
                            },
                            typePrice = new CreateStockRequest.stockStockHeaderTypePrice
                            {
                                ids = "SK"
                            },
                            limitMin = 0,
                            limitMax = 1000,
                            mass = 0,
                            supplier = new CreateStockRequest.stockStockHeaderSupplier(),
                            note = "Importovane z xml"
                        }
                    }
                }
            }
        };
    }

    public CreateStockRequest.dataPack BuildFromCreteOrderData(StockData createOrderData)
    {
        var stock = new StockBuilder()
            .WithCode(createOrderData.Code)
            .WithName(createOrderData.Name)
            .WithPrice(createOrderData.Price)
            .WithEan(createOrderData.Ean)
            .WithManufacturer(createOrderData.Manufacturer)
            .WithDescription(createOrderData.Description)
            .WithPicture(createOrderData.ImgFilePath, createOrderData.ImgUrl)
            .WithRelatedLink(createOrderData.RelatedLink);

        if (createOrderData.AlternativeImages is not null)
        {
            stock = stock.WithAlternativePictures(createOrderData.AlternativeImages, createOrderData.ImgFilePath);
        }

        if (createOrderData.Supplier is not null)
        {
            stock = stock.WithSupplier(createOrderData.Supplier.Value);
        }

        return stock.Build();
    }

    public CreateStockRequest.dataPack Build()
    {
        return _stock;
    }

    public StockBuilder WithCode(string code)
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
        _stock.dataPackItem[0].stock.stockHeader.purchasingPrice = price;
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

    public StockBuilder WithPicture(string picturePath, string pictureName)
    {
        _stock.dataPackItem[0].stock.stockHeader.pictures = new CreateStockRequest.stockStockHeaderPictures
        {
            picture = new CreateStockRequest.stockStockHeaderPicturesPicture[]
            {
                new()
                {
                    filepath = picturePath + pictureName.Split("/").Last(),
                    @default = true,
                    description = "obrazok produktu"
                }
            }
        };
        return this;
    }

    public StockBuilder WithAlternativePictures(IEnumerable<string> pictures, string picturePath)
    {
        var pics = _stock.dataPackItem[0].stock.stockHeader.pictures.picture.ToList();
        pics.AddRange(pictures.Select(picture => new CreateStockRequest.stockStockHeaderPicturesPicture
            { filepath = picturePath + picture.Split("/").Last(), @default = false, description = "obrazok produktu" }));
        _stock.dataPackItem[0].stock.stockHeader.pictures.picture = pics.ToArray();
        return this;
    }

    public StockBuilder WithRelatedLink(string link)
    {
        _stock.dataPackItem[0].stock.stockHeader.relatedLinks = new CreateStockRequest.stockStockHeaderRelatedLinks
        {
            relatedLink = new CreateStockRequest.stockStockHeaderRelatedLinksRelatedLink
            {
                description = "odkaz na produkt",
                order = 1,
                addressURL = link
            }
        };

        return this;
    }

    public StockBuilder WithSupplier(byte id)
    {
        _stock.dataPackItem[0].stock.stockHeader.supplier.id = id;
        return this;
    }
}