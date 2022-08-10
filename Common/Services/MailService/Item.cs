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

    public string ItemId { get; }
    public string Ean { get; }
    public string Url { get; }
    public string Dealer { get; }
}