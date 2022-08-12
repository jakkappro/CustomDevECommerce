using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Services;

namespace PacketaConnector.Interfaces;

public interface ICarrier
{
    Task CreatePackage(Packet packet);

    Task<string> GetLabel(uint id);

    Task<GetPacketStatusResponse.response> GetPackageInfo(uint id);
}