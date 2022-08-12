#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.GetPacketStatus;

public class GetPacketStatusRequest
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class packetStatus
    {
        private string apiPasswordField;

        private uint packetIdField;

        public string apiPassword
        {
            get => apiPasswordField;
            set => apiPasswordField = value;
        }

        public uint packetId
        {
            get => packetIdField;
            set => packetIdField = value;
        }
    }
}