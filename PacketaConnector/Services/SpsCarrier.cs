

using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Interfaces;
using Webship;
using Webtrace;

namespace PacketaConnector.Services
{
    public class SpsCarrier : ICarrier
    {
        private readonly WebshipWebServiceClient webshipClient;
        private readonly WebtracePortTypeClient webtraceClient;
        
        public Task<string> CreatePackage(Packet packet)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLabel(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GetPacketStatusResponse.response> GetPackageInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
