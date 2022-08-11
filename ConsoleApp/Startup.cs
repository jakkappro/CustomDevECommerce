using System.Text;
using Common.Interfaces;
using Common.Services.ImageDownloadService;
using Common.Services.MailService;
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
        services.AddHttpClient<IXmlFeedParser, UrlXmlFeedParser>();
        services.AddHttpClient<IDownloader, ImageDownloader>();
        services.AddTransient<IMailSender, MailSender>();

        // Add expando services
        services.AddTransient<IExpandoOrder, ExpandoOrderService>();

        // Add packeta services
        services.AddTransient<IPacketBuilder, PacketBuilder>();
        services.AddHttpClient<ICarrier, PacketaCarrier>(client =>
        {
            client.BaseAddress = new Uri("https://www.zasilkovna.cz/api/rest/");
        });

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
        services.AddTransient<IStarterService, StarterService>();
    }
}