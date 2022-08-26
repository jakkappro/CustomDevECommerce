using Microsoft.Extensions.Configuration;

namespace XMLFeedSynchronizer;

using System.Text;
using Common.Interfaces;
using Common.Services.ImageDownloadService;
using Common.Services.Serialization;
using Common.Services.XMLFeedParserService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PohodaConnector.Builders.Orders;
using PohodaConnector.Builders.Stock;
using PohodaConnector.Interfaces;
using PohodaConnector.Services.MServer;
using PohodaConnector.Services.OrderService;
using PohodaConnector.Services.StockService;
using Serilog;

public class Startup
{
    public static void Initialize(string[] args)
    {
        InitializeLogger();
        
        Log.Information("Initializing application.");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(CreateServices)
            .UseSerilog()
            .Build();


        IStarterService app;
        
        app = ActivatorUtilities.CreateInstance<Starter>(host.Services);

        app.Run().Wait();
    }

    private static void InitializeLogger()
    {
        var builder = new ConfigurationBuilder();

        builder.AddJsonFile("appsettings.json", false, true);
        builder.AddEnvironmentVariables();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }

    private static void CreateServices(HostBuilderContext context, IServiceCollection services)
    {
        // Add common services
        services.AddSingleton<ISerializer, Utf8SerializerService>();
        services.AddHttpClient<IXmlFeedParser, UrlXmlFeedParser>();
        services.AddSingleton<IDownloader, ImageDownloader>();

        // Add pohoda services
        services.AddHttpClient<IAccountingSoftware, MServer>(client =>
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5336");
            client.DefaultRequestHeaders.Add("STW-Authorization",
                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    $"{context.Configuration["Pohoda:username"]}:{context.Configuration["Pohoda:password"]}")));
            client.DefaultRequestHeaders.Add("Accept", "text/xml");
        });

        services.AddTransient<IGetOrdersByDateRequestBuilder, GetOrdersByDateRequestBuilder>();
        services.AddTransient<IOrderBuilder, OrderBuilder>();
        services.AddTransient<IGetStockRequestBuilder, GetStockRequestBuilder>();
        services.AddTransient<IStockBuilder, StockBuilder>();

        services.AddTransient<IOrderService, OrderService>();

        services.AddTransient<IStockService, StockService>();

        // Add starter service
        services.AddTransient<IStarterService, Starter>();
    }
}