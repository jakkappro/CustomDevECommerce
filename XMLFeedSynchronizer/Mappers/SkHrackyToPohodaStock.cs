using PohodaConnector.Services.StockService;
using XMLFeedSynchronizer.XMLFeeds;

namespace XMLFeedSynchronizer.Mappers;

public class SkHrackyToPohodaStock
{
    public static StockData Map(SkHracky.SHOPSHOPITEM item)
    {
        return new StockData(
            item.IMGURL,
            uint.Parse(item.ITEMID),
            item.EAN,
            item.PRICE,
            item.MANUFACTURER,
            item.DESCRIPTION,
            item.PRODUCT,
            @"\\AzetCool-Pohoda\POHODA_SK_E1_DATA\Dokumenty\ACecom\Obrázky\",
            item.URL,
            item.IMGURL_ALTERNATIVE
        );
    }
}