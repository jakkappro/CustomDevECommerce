#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.CreateOrders;

public class CreateOrderResponse
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
        private long idField;

        private string barcodeField;

        private string barcodeTextField;

        public long id
        {
            get => idField;
            set => idField = value;
        }

        public string barcode
        {
            get => barcodeField;
            set => barcodeField = value;
        }

        public string barcodeText
        {
            get => barcodeTextField;
            set => barcodeTextField = value;
        }
    }
}