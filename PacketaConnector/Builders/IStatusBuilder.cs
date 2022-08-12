using PacketaConnector.DTO.GetPacketStatus;

namespace PacketaConnector.Builders;

public interface IStatusBuilder
{
    GetPacketStatusRequest.packetStatus BuildFromCreteOrderData(uint packetId, string apiPassword);
    StatusBuilder WithId(uint packetId);
    GetPacketStatusRequest.packetStatus Build();
    StatusBuilder WithApiPassword(string password);
}