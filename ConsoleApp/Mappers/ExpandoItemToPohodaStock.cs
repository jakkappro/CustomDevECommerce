using ExpandoConnector.DTO.PrehomeFeed;
using PohodaConnector.Enums;
using PohodaConnector.Services.StockService;

namespace ConsoleApp.Mappers;

public static class ExpandoItemToPohodaStock
{
    public static StockData Map(GetPrehomeFeed.SHOPSHOPITEM item)
    {
        return new StockData(
            item.IMGURL,
            item.ITEM_ID.ToString(),
            item.EAN,
            item.PRICE,
            item.MANUFACTURER,
            item.DESCRIPTION,
            item.PRODUCTNAME,
            @"\\AzetCool-Pohoda\POHODA_SK_E1_DATA\Dokumenty\ACecom\Obrázky\",
            item.URL,
            null,
            (byte)Suppliers.Prehome
        );
    }
}