using Common.Interfaces;
using Microsoft.Extensions.Logging;
using PohodaConnector.Interfaces;
using PohodaConnector.Services.StockService;
using XMLFeedSynchronizer.Mappers;
using XMLFeedSynchronizer.XMLFeeds;

namespace XMLFeedSynchronizer;

public class Starter : IStarterService
{
    private readonly IXmlFeedParser _xmlFeedParser;
    private readonly IStockService _stockService;
    private readonly ILogger<Starter> _logger;

    public Starter(IXmlFeedParser xmlFeedParser, IStockService stockService,
        ILogger<Starter> logger)
    {
        _xmlFeedParser = xmlFeedParser;
        _stockService = stockService;
        _logger = logger;
    }

    public async Task Run()
    {
        var feed = _xmlFeedParser.Parse<SkHracky.SHOP>("https://www.hrackys.cz/XmlFeed/SKShopHrackysXMLPartners.xml");
        _logger.LogInformation("Parsed skHracky feed");

        await _stockService.Initialize();
        _logger.LogInformation("Initialized stock service");

        var times = 4;
        
        foreach (var shopItem in feed.SHOPITEM)
        {
            if (times <= 0)
            {
                break;
            }

            times--;
            if (! await _stockService.Exists(shopItem.ITEMID))
            {
                // create stock
                _stockService.CreateStock(SkHrackyToPohodaStock.Map(shopItem));
                continue;
            }
            
            // check whether stock has changed
        }
    }
}