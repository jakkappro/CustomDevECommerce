using ConsoleApp.Poco;
using ExpandoConnector.Interfaces;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp.ApplicationModes;

public class ExpandoSynchronizeMode : IStarterService
{
    private readonly IConfiguration _configuration;
    private readonly IExpandoOrder _servicExpadoOrder;

    public ExpandoSynchronizeMode(IConfiguration configuration, IExpandoOrder servicExpadoOrder)
    {
        _configuration = configuration;
        _servicExpadoOrder = servicExpadoOrder;
    }

    public void Run()
    {

        // get data
        using var db = new LiteDatabase(_configuration["Database:Path"]);

        var col = db.GetCollection<Data>("customers");


        var results = col.Query()
            .Where(x => x.Status.Equals("Unshipped"))
            .ToList();

        foreach (var result in from order in results where order.PackageTrackingNumber is not null select _servicExpadoOrder.UpdateOrder(new()
                 {
                     MarketplaceOrderId = order.AmazonOrderId,
                     Marketplace = order.MarketPlace,
                     Status = "Shipped",
                     TrackingNumber = long.Parse(order.PackageTrackingNumber),
                     // Carrier = order.CarrierInfo,
                     Carrier = "Other",
                     CarrierName = "ExpressOne"
                 }).Result)
        {
            Console.WriteLine(result);
        }

    }
}