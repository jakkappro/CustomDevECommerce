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

        private responseResult resultField;

           public string status
        {
            get => statusField;
            set => statusField = value;
        }

           public responseResult result
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

        private byte statusCodeField;

        private string codeTextField;

        private string statusTextField;

        private byte branchIdField;

        private byte destinationBranchIdField;

        private string externalTrackingCodeField;

        private byte isReturningField;

        private string storedUntilField;

        private ushort carrierIdField;

        private string carrierNameField;

           public DateTime dateTime
        {
            get => dateTimeField;
            set => dateTimeField = value;
        }

           public byte statusCode
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

           public byte branchId
        {
            get => branchIdField;
            set => branchIdField = value;
        }

           public byte destinationBranchId
        {
            get => destinationBranchIdField;
            set => destinationBranchIdField = value;
        }

           public string externalTrackingCode
        {
            get => externalTrackingCodeField;
            set => externalTrackingCodeField = value;
        }

           public byte isReturning
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