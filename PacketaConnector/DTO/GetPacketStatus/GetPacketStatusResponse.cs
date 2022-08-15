#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.GetPacketStatus;

public class GetPacketStatusResponse
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class response
    {
        private string statusField;

        private responseResult? resultField;

        public string status
        {
            get => statusField;
            set => statusField = value;
        }

        public responseResult? result
        {
            get => resultField;
            set => resultField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class responseResult
    {
        private DateTime dateTimeField;

        private string statusCodeField;

        private string codeTextField;

        private string statusTextField;

        private string branchIdField;

        private string destinationBranchIdField;

        private string? externalTrackingCodeField;

        private string? isReturningField;

        private string? storedUntilField;

        private ushort carrierIdField;

        private string carrierNameField;

        public DateTime dateTime
        {
            get => dateTimeField;
            set => dateTimeField = value;
        }

        public string statusCode
        {
            get => statusCodeField;
            set => statusCodeField = value;
        }

        public string codeText
        {
            get => codeTextField;
            set => codeTextField = value;
        }

        public string statusText
        {
            get => statusTextField;
            set => statusTextField = value;
        }

        public string branchId
        {
            get => branchIdField;
            set => branchIdField = value;
        }

        public string destinationBranchId
        {
            get => destinationBranchIdField;
            set => destinationBranchIdField = value;
        }

        public string? externalTrackingCode
        {
            get => externalTrackingCodeField;
            set => externalTrackingCodeField = value;
        }

        public string isReturning
        {
            get => isReturningField;
            set => isReturningField = value;
        }

        public string storedUntil
        {
            get => storedUntilField;
            set => storedUntilField = value;
        }

        public ushort carrierId
        {
            get => carrierIdField;
            set => carrierIdField = value;
        }

        public string carrierName
        {
            get => carrierNameField;
            set => carrierNameField = value;
        }
    }
}