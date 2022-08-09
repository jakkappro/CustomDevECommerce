#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PacketaConnector.DTO.CreateOrders;

public class CreateOrderRequest
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class createPacket
    {
        private string apiPasswordField;

        private createPacketPacketAttributes packetAttributesField;

        public string apiPassword
        {
            get => apiPasswordField;
            set => apiPasswordField = value;
        }

        public createPacketPacketAttributes packetAttributes
        {
            get => packetAttributesField;
            set => packetAttributesField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class createPacketPacketAttributes
    {
        private string numberField;

        private string nameField;

        private string surnameField;

        private string emailField;

        private uint addressIdField;

        private decimal valueField;

        private string eshopField;

        private decimal weightField;

        private uint sender_idField;

        private string phoneField;

        private string zipField;

        private string streetField;

        private string houseNumberField;

        private string cityField;

        private createPacketPacketAttributesSecurity securityField;

        private string currencyField;

        public string number
        {
            get => numberField;
            set => numberField = value;
        }

        public string name
        {
            get => nameField;
            set => nameField = value;
        }

        public string surname
        {
            get => surnameField;
            set => surnameField = value;
        }

        public string email
        {
            get => emailField;
            set => emailField = value;
        }

        public uint addressId
        {
            get => addressIdField;
            set => addressIdField = value;
        }

        public decimal value
        {
            get => valueField;
            set => valueField = value;
        }

        public string eshop
        {
            get => eshopField;
            set => eshopField = value;
        }

        public decimal weight
        {
            get => weightField;
            set => weightField = value;
        }

        public uint sender_id
        {
            get => sender_idField;
            set => sender_idField = value;
        }

        public string phone
        {
            get => phoneField;
            set => phoneField = value;
        }

        public string zip
        {
            get => zipField;
            set => zipField = value;
        }

        public string street
        {
            get => streetField;
            set => streetField = value;
        }

        public string houseNumber
        {
            get => houseNumberField;
            set => houseNumberField = value;
        }

        public string city
        {
            get => cityField;
            set => cityField = value;
        }

        public createPacketPacketAttributesSecurity security
        {
            get => securityField;
            set => securityField = value;
        }

        public string currency
        {
            get => currencyField;
            set => currencyField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class createPacketPacketAttributesSecurity
    {
        private byte allowPublicTrackingField;

        public byte allowPublicTracking
        {
            get => allowPublicTrackingField;
            set => allowPublicTrackingField = value;
        }
    }
}