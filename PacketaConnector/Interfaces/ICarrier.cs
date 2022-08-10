using PacketaConnector.Services;

namespace PacketaConnector.Interfaces;

public interface ICarrier
{
    void CreatePackage(Packet packet);

    void GetLabel();

    void GetPackageInfo();

    
}