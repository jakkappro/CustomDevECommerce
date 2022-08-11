using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = Startup.Initialize();
        Log.Logger.Information("Starting app.");
        var app = ActivatorUtilities.CreateInstance<StarterService>(host.Services);
        app.Run();
    }
}