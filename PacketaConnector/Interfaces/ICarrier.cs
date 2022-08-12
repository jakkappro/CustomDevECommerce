using PacketaConnector.Services;

namespace PacketaConnector.Interfaces;

public interface ICarrier
{
    Task CreatePackage(Packet packet);

    void GetLabel();

    void GetPackageInfo();
}