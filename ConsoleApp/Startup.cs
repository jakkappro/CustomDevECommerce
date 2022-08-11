using Common.Interfaces;
using Common.Services.Serialization;
using Common.Services.XMLFeedParserService;
using ExpandoConnector.Interfaces;
using ExpandoConnector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PacketaConnector.Builders;
using PacketaConnector.Interfaces;
using PacketaConnector.Services;
using PohodaConnector.Builders.Orders;
using PohodaConnector.Builders.Stock;
using PohodaConnector.Interfaces;
using PohodaConnector.Services.MServer;
using PohodaConnector.Services.OrderService;
using PohodaConnector.Services.StockService;
using Serilog;

namespace ConsoleApp;

public class Startup
{
    public static IHost Initialize()
    {
        var builder = new ConfigurationBuilder();

        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.AddEnvironmentVariables();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Starting application.");

        return Host.CreateDefaultBuilder()
            .ConfigureServices(CreateServices)
            .UseSerilog()
            .Build(); 
    }

    private static void CreateServices(HostBuilderContext context, IServiceCollection services)
    {
        // Add common services
        services.AddSingleton<ISerializer, Utf8SerializerService>();
        services.AddHttpClient<IXmlFeedParser, UrlXmlFeedParser>(client =>
        {
            // configuration here
        });

        // Add expando services
        services.AddHttpClient<IExpandoOrder, ExpandoOrderService>(client =>
        {
            // configuration here
        });

        // Add packeta services
        services.AddTransient<IPacketBuilder, PacketBuilder>();
        services.AddHttpClient<ICarrier, PacketaCarrier>(client =>
        {
            // configuration here
        });

        // Add pohoda services
        services.AddHttpClient<IAccountingSoftware, MServer>();

        services.AddTransient<IGetOrdersByDateRequestBuilder, GetOrdersByDateRequestBuilder>();
        services.AddTransient<IOrderBuilder, OrderBuilder>();
        services.AddTransient<IGetStockRequestBuilder, GetStockRequestBuilder>();
        services.AddTransient<IStockBuilder, StockBuilder>();

        services.AddTransient<IOrderService, OrderService>();

        services.AddTransient<IStockService, StockService>();

        // Add starter service
        services.AddTransient<IStarterService, StarterService>();

    }
}