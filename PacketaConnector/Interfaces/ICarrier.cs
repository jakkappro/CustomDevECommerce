using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Services;

namespace PacketaConnector.Interfaces;

public interface ICarrier
{
    Task<string> CreatePackage(Packet packet);

    Task<string> GetLabel(string id);

    Task<GetPacketStatusResponse.response> GetPackageInfo(string id);
}