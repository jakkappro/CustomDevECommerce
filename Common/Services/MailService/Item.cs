#pragma warning disable CS8618
namespace Common.Services.MailService;

public class Item
{
    public Item(string itemId, string ean, string url, string dealer)
    {
        ItemId = itemId;
        Ean = ean;
        Url = url;
        Dealer = dealer;
    }

    public Item()
    {
    }

    public string ItemId { get; set; }
    public string Ean { get; set; }
    public string Url { get; set; }
    public string Dealer { get; set; }
}