
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
                            pictures = new CreateStockRequest.stockStockHeaderPictures
                            {
                                picture = new CreateStockRequest.stockStockHeaderPicturesPicture
                                {
                                    @default = true,
                                    description = "obrazok produktu",
                                }
                            },
                            note = "Importovane z xml",
                            relatedLinks = new CreateStockRequest.stockStockHeaderRelatedLinks
                            {
                                relatedLink = new CreateStockRequest.stockStockHeaderRelatedLinksRelatedLink
                                {
                                    description = "odkaz na produkt",
                                    order = 1
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    public CreateStockRequest.dataPack BuildFromCreteOrderData(StockData createOrderData)
    {
        var order = new StockBuilder();

        return order.Build();
    }

    public CreateStockRequest.dataPack Build()
    {
        return _stock;
    }
}