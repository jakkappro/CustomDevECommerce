using ExpandoConnector.DTO.ExpandoFeed;
using ExpandoConnector.DTO.PrehomeFeed;
using PohodaConnector.Services.StockService;

namespace ConsoleApp.Mappers;

public static class ExpandoItemToPohodaStock
{
    public static StockData Map(GetPrehomeFeed.SHOPSHOPITEM item)
    {
        return new StockData(
            item.IMGURL,
            item.ITEM_ID,
            item.EAN,
            item.PRICE * 1.2m,
            item.MANUFACTURER,
            item.DESCRIPTION,
            item.PRODUCTNAME,
            "Cesta ku suboru :D",
            item.URL
        );
    } 
}