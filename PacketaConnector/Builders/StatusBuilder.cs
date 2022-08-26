using PacketaConnector.DTO.GetPacketStatus;

namespace PacketaConnector.Builders;

public class StatusBuilder : IStatusBuilder
{
    private readonly GetPacketStatusRequest.packetStatus _status;

    public StatusBuilder()
    {
        _status = new GetPacketStatusRequest.packetStatus();
    }

    public GetPacketStatusRequest.packetStatus BuildFromCreateOrderData(string packetId, string apiPassword)
    {
        return new StatusBuilder().WithApiPassword(apiPassword)
            .WithId(packetId)
            .Build();
    }

    public StatusBuilder WithId(string packetId)
    {
        _status.packetId = packetId;
        return this;
    }

    public GetPacketStatusRequest.packetStatus Build()
    {
        return _status;
    }

    public StatusBuilder WithApiPassword(string password)
    {
        _status.apiPassword = password;
        return this;
    }
}