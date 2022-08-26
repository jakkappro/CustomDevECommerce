using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PacketaConnector.Builders;
using PacketaConnector.DTO.CreateOrders;
using PacketaConnector.DTO.GenerateLabels;
using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Interfaces;

namespace PacketaConnector.Services;

public class PacketaCarrier : ICarrier
{
    private readonly IPacketBuilder _builder;
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILabelBuilder _labelBuilder;
    private readonly ILogger<PacketaCarrier> _logger;
    private readonly ISerializer _serializer;
    private readonly IStatusBuilder _statusBuilder;
    private int _retries;

    public PacketaCarrier(IPacketBuilder builder, HttpClient client, ISerializer serializer,
        ILogger<PacketaCarrier> logger, IConfiguration configuration, IStatusBuilder statusBuilder,
        ILabelBuilder labelBuilder)
    {
        _serializer = serializer;
        _logger = logger;
        _configuration = configuration;
        _statusBuilder = statusBuilder;
        _labelBuilder = labelBuilder;
        _builder = builder;
        _client = client;
        _retries = 3;
    }

    public async Task<string> CreatePackage(Packet packet)
    {
        try
        {
            var fromCreateOrderData = _builder.BuildFromCreateOrderData(packet, _configuration["Packeta:ApiPassword"]);
            _logger.LogDebug("Creating packet: {packet}", fromCreateOrderData);
            var buildFromCreateOrderData = _serializer.Serialize(fromCreateOrderData);
            var response = await _client.PostAsync("",
                new StringContent(buildFromCreateOrderData));
            var responseString = await response.Content.ReadAsStringAsync();
            var id = _serializer.Deserialize<CreateOrderResponse.response>(responseString);
            if (id.status.Equals("ok"))
                return id.result!.id;

            _logger.LogError("Couldn't create packet.");
            throw new ArgumentException();
        }
        catch
        {
            _logger.LogError("Failed to create package.");
            throw;
        }
    }

    public async Task<string> GetLabel(string id)
    {
        _retries = 3;
        var response = await GetPackageInfo(id);

        while (response.result is null && _retries > 0)
        {
            response = await GetPackageInfo(id);
            Thread.Sleep(500);
            _retries--;
        }

        if (_retries == 0)
        {
            _logger.LogError("Couldn't load data for package id: {id}", id);
            throw new ArgumentException();
        }

        var number = response.result?.externalTrackingCode ?? id;

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

        var sPdfDecoded = Convert.FromBase64String(label);
        await File.WriteAllBytesAsync($@"{_configuration["Packeta:LabelsLocation"]}\{number}.pdf", sPdfDecoded);

        return $"{number}.pdf";
    }

    public async Task<GetPacketStatusResponse.response> GetPackageInfo(string id)
    {
        try
        {
            var fromCreateOrderData =
                _statusBuilder.BuildFromCreateOrderData(id, _configuration["Packeta:ApiPassword"]);
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