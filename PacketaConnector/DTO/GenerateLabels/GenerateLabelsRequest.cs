using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.GenerateLabels;

public class GenerateLabelRequest
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class packetLabelPdf
    {
        private string apiPasswordField;

        private long packetIdField;

        private string formatField;

        private byte offsetField;

        public string apiPassword
        {
            get => apiPasswordField;
            set => apiPasswordField = value;
        }

        public long packetId
        {
            get => packetIdField;
            set => packetIdField = value;
        }

        public string format
        {
            get => formatField;
            set => formatField = value;
        }

        public byte offset
        {
            get => offsetField;
            set => offsetField = value;
        }

        public static string Serialize(packetLabelPdf data)
        {
            var x = new XmlSerializer(data.GetType());

            TextWriter writer = new StringWriter();
            x.Serialize(writer, data);
            var s = writer.ToString() ?? throw new InvalidOperationException();
            writer.Flush();
            writer.Close();
            return s;
        }
    }
}