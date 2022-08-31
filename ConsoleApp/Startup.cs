using System.Text;
using Common.Interfaces;
using Common.Services.IdGenerator;
using Common.Services.ImageDownloadService;
using Common.Services.MailService;
using Common.Services.Serialization;
using Common.Services.XMLFeedParserService;
using ConsoleApp.ApplicationModes;
using ExpandoConnector.Interfaces;
using ExpandoConnector.Services;
using Fclp;
using Fclp.Internals.Extensions;
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
    public static void Initialize(string[] args)
    {
        InitializeLogger();

        var options = GetApplicationOptions(args);

        Log.Information("Initializing application.");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(options.IsMailOnly ? CreateMailServices : CreateAllServices)
            .UseSerilog()
            .Build();


        IStarterService app;

        if (options.IsMailOnly)
            app = ActivatorUtilities.CreateInstance<MailMode>(host.Services, options.LookBackDays, false);
        else if (options.IsExpandoSynch)
            app = ActivatorUtilities.CreateInstance<ExpandoSynchronizeMode>(host.Services);
        else
            app = ActivatorUtilities.CreateInstance<FullMode>(host.Services, options.LookBackDays, false);

        app.Run();
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

    private static ApplicationArguments GetApplicationOptions(string[] args)
    {
        var parser = new FluentCommandLineParser<ApplicationArguments>();

        parser.SetupHelp("?", "help");


        parser.Setup(arg => arg.LookBackDays)
            .As('d', "number-of-days")
            .SetDefault(1)
            .WithDescription("Sets amount of days to look back for new orders.");

        parser.Setup(arg => arg.IsMailOnly)
            .As('m', "mail")
            .SetDefault(true)
            .WithDescription("If true sets application to mail only mode.");

        parser.Setup(arg => arg.IsExpandoSynch)
            .As('e', "expando-synch")
            .SetDefault(false)
            .WithDescription("If true sets application to expando synch mode.");

        var result = parser.Parse(args);

        if (!result.Errors.IsNullOrEmpty()) throw new ArgumentNullException();

        return parser.Object;
    }

    private static void CreateMailServices(HostBuilderContext context, IServiceCollection services)
    {
        // Add common services
        services.AddSingleton<ISerializer, Utf8SerializerService>();
        services.AddHttpClient<IXmlFeedParser, UrlXmlFeedParser>();
        services.AddTransient<IMailSender, MailSender>();
        services.AddSingleton<IIdGenerator, IdGenerator>();
        services.AddSingleton<IDownloader, ImageDownloader>();

        // Add expando services
        services.AddTransient<IExpandoOrder, ExpandoOrderService>();

        // Add packeta services
        services.AddTransient<IPacketBuilder, PacketBuilder>();
        services.AddTransient<IStatusBuilder, StatusBuilder>();
        services.AddTransient<ILabelBuilder, LabelBuilder>();
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
        services.AddTransient<IStarterService, MailMode>();
    }

    private static void CreateAllServices(HostBuilderContext context, IServiceCollection services)
    {
        // TODO: delete this 
    }

    public class ApplicationArguments
    {
        public int LookBackDays { get; set; }
        public bool IsMailOnly { get; set; }
        public bool IsExpandoSynch { get; set; }
    }
}