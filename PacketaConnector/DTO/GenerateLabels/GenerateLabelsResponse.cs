#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.GenerateLabels;

public class PacketaGenerateLabelResponse
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class response
    {
        private string statusField;

        private string resultField;

        public string status
        {
            get => statusField;
            set => statusField = value;
        }

        public string result
        {
            get => resultField;
            set => resultField = value;
        }
    }
}