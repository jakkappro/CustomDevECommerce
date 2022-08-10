using Common.Interfaces;
using Microsoft.Extensions.Logging;
using PacketaConnector.Builders;
using PacketaConnector.Interfaces;

namespace PacketaConnector.Services;

public class PacketaCarrier : ICarrier
{
    private readonly ISerializer _serializer;
    private readonly ILogger<PacketaCarrier> _logger;
    private readonly PacketBuilder _builder;
    private readonly HttpClient _client;

    public PacketaCarrier(PacketBuilder builder, HttpClient client, ISerializer serializer, ILogger<PacketaCarrier> logger)
    {
        _serializer = serializer;
        _logger = logger;
        _builder = builder;
        _client = client;
    }

    public void CreatePackage(Packet packet)
    {
        try
        {
            _client.PostAsync("https://www.zasilkovna.cz/api/rest/",
                new StringContent(_serializer.Serialize(_builder.BuildFromCreteOrderData(packet))));
        }
        catch
        {
            _logger.LogError("Failed to create package.");
            throw;
        }
    }

    public void GetLabel()
    {
        throw new NotImplementedException();
    }

    public void GetPackageInfo()
    {
        throw new NotImplementedException();
    }
}