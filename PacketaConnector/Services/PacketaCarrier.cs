using Common.Interfaces;
using PacketaConnector.Builders;
using PacketaConnector.Interfaces;

namespace PacketaConnector.Services;

public class PacketaCarrier : ICarrier
{
    private readonly ISerializer _serializer;
    private readonly PacketBuilder _builder;
    private readonly HttpClient _client;

    public PacketaCarrier(PacketBuilder builder, HttpClient client, ISerializer serializer)
    {
        _serializer = serializer;
        _builder = builder;
        _client = client;
    }

    public void CreatePackage(Packet packet)
    {
        _client.PostAsync("https://www.zasilkovna.cz/api/rest/",
            new StringContent(_serializer.Serialize(_builder.BuildFromCreteOrderData(packet))));
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