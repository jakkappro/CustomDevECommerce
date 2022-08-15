using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Builders;
using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Interfaces;
using PacketaConnector.DTO.GenerateLabels;

namespace PacketaConnector.Services;

public class PacketaCarrier : ICarrier
{
    private readonly ISerializer _serializer;
    private readonly ILogger<PacketaCarrier> _logger;
    private readonly IConfiguration _configuration;
    private readonly IStatusBuilder _statusBuilder;
    private readonly ILabelBuilder _labelBuilder;
    private readonly IPacketBuilder _builder;
    private readonly HttpClient _client;

    public PacketaCarrier(IPacketBuilder builder, HttpClient client, ISerializer serializer, ILogger<PacketaCarrier> logger, IConfiguration configuration, IStatusBuilder statusBuilder, ILabelBuilder labelBuilder)
    {
        _serializer = serializer;
        _logger = logger;
        _configuration = configuration;
        _statusBuilder = statusBuilder;
        _labelBuilder = labelBuilder;
        _builder = builder;
        _client = client;
    }

    public async Task CreatePackage(Packet packet)
    {
        try
        {
            var fromCreateOrderData = _builder.BuildFromCreateOrderData(packet, _configuration["Packeta:ApiPassword"]);
            var buildFromCreateOrderData = _serializer.Serialize(fromCreateOrderData);
            var response = await _client.PostAsync("",
                new StringContent(buildFromCreateOrderData));
            // TODO: check response for errors 
            // var readAsStringAsync = await response.Content.ReadAsStringAsync();
        }
        catch
        {
            _logger.LogError("Failed to create package.");
            throw;
        }
    }

    public async Task<string> GetLabel(uint id)
    {
        // get tracking number
        var number = (await GetPackageInfo(id)).result.externalTrackingCode;
        
        // get label
        string label;
        try
        {
            var fromCreateOrderData = _labelBuilder.BuildFromCreateOrderData(id, _configuration["Packeta:ApiPassword"]);
            var buildFromCreateOrderData = _serializer.Serialize(fromCreateOrderData);
            var r = await (await _client.PostAsync("",
                new StringContent(buildFromCreateOrderData))).Content.ReadAsStringAsync();
            label = _serializer.Deserialize<GenerateLabelResponse.response>(r).result;
        }
        catch
        {
            _logger.LogError("Couldn't get status from packet: {id}.", id);
            throw;
        }

        // create file

        var sPdfDecoded = Convert.FromBase64String(label);
        await File.WriteAllBytesAsync($@"labels\{number}.pdf", sPdfDecoded);

        return $"{number}.pdf";
    }

    public async Task<GetPacketStatusResponse.response> GetPackageInfo(uint id)
    {
        try
        {
            var fromCreateOrderData = _statusBuilder.BuildFromCreateOrderData(id, _configuration["Packeta:ApiPassword"]);
            var buildFromCreateOrderData = _serializer.Serialize(fromCreateOrderData);
            var response = await (await _client.PostAsync("",
                new StringContent(buildFromCreateOrderData))).Content.ReadAsStringAsync();

            return _serializer.Deserialize<GetPacketStatusResponse.response>(response);
        }
        catch
        {
            _logger.LogError("Couldn't get status from packet: {id}.", id);
            throw;
        }
    }
}