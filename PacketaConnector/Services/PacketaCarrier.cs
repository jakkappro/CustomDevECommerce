using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Builders;
using PacketaConnector.Interfaces;

namespace PacketaConnector.Services;

public class PacketaCarrier : ICarrier
{
    private readonly ISerializer _serializer;
    private readonly ILogger<PacketaCarrier> _logger;
    private readonly IConfiguration _configuration;
    private readonly IPacketBuilder _builder;
    private readonly HttpClient _client;

    public PacketaCarrier(IPacketBuilder builder, HttpClient client, ISerializer serializer, ILogger<PacketaCarrier> logger, IConfiguration configuration)
    {
        _serializer = serializer;
        _logger = logger;
        _configuration = configuration;
        _builder = builder;
        _client = client;
    }

    public async Task CreatePackage(Packet packet)
    {
        try
        {
            var fromCreteOrderData = _builder.BuildFromCreteOrderData(packet, _configuration["Packeta:ApiPassword"]);
            var buildFromCreteOrderData = _serializer.Serialize(fromCreteOrderData);
            var response = await _client.PostAsync("",
                new StringContent(buildFromCreteOrderData));
            // TODO: check response for errors 
            // var readAsStringAsync = await response.Content.ReadAsStringAsync();
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