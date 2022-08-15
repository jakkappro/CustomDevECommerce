using PacketaConnector.DTO.GetPacketStatus;

namespace PacketaConnector.Builders;

public interface IStatusBuilder
{
    GetPacketStatusRequest.packetStatus BuildFromCreateOrderData(string packetId, string apiPassword);
    StatusBuilder WithId(string packetId);
    GetPacketStatusRequest.packetStatus Build();
    StatusBuilder WithApiPassword(string password);
}