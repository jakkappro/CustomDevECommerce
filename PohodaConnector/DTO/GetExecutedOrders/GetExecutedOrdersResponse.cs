#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PohodaConnector.DTO.GetExecutedOrders;

public class GetExecutedOrdersResponse
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/response.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/response.xsd", IsNullable = false)]
    public class responsePack
    {
        private uint icoField;

        private byte idField;

        private string keyField;

        private string noteField;

        private string programVersionField;
        private responsePackResponsePackItem responsePackItemField;

        private string stateField;

        private decimal versionField;

        public responsePackResponsePackItem responsePackItem
        {
            get => responsePackItemField;
            set => responsePackItemField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }

        [XmlAttribute]
        public byte id
        {
            get => idField;
            set => idField = value;
        }

        [XmlAttribute]
        public string state
        {
            get => stateField;
            set => stateField = value;
        }

        [XmlAttribute]
        public string programVersion
        {
            get => programVersionField;
            set => programVersionField = value;
        }

        [XmlAttribute]
        public uint ico
        {
            get => icoField;
            set => icoField = value;
        }

        [XmlAttribute]
        public string key
        {
            get => keyField;
            set => keyField = value;
        }

        [XmlAttribute]
        public string note
        {
            get => noteField;
            set => noteField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/response.xsd")]
    public class responsePackResponsePackItem
    {
        private string idField;
        private listOrder listOrderField;

        private string stateField;

        private decimal versionField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/list.xsd")]
        public listOrder listOrder
        {
            get => listOrderField;
            set => listOrderField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }

        [XmlAttribute]
        public string id
        {
            get => idField;
            set => idField = value;
        }

        [XmlAttribute]
        public string state
        {
            get => stateField;
            set => stateField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/list.xsd", IsNullable = false)]
    public class listOrder
    {
        private DateTime dateTimeStampField;

        private DateTime dateValidFromField;
        private listOrderOrder[] orderField;

        private string stateField;

        private decimal versionField;

        [XmlElement("order")]
        public listOrderOrder[] order
        {
            get => orderField;
            set => orderField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }

        [XmlAttribute]
        public DateTime dateTimeStamp
        {
            get => dateTimeStampField;
            set => dateTimeStampField = value;
        }

        [XmlAttribute(DataType = "date")]
        public DateTime dateValidFrom
        {
            get => dateValidFromField;
            set => dateValidFromField = value;
        }

        [XmlAttribute]
        public string state
        {
            get => stateField;
            set => stateField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list.xsd")]
    public class listOrderOrder
    {
        private orderDetailOrderItem[] orderDetailField;
        private orderHeader orderHeaderField;

        private orderSummary orderSummaryField;

        private decimal versionField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
        public orderHeader orderHeader
        {
            get => orderHeaderField;
            set => orderHeaderField = value;
        }

        [XmlArray(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
        [XmlArrayItem("orderItem", IsNullable = false)]
        public orderDetailOrderItem[] orderDetail
        {
            get => orderDetailField;
            set => orderDetailField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
        public orderSummary orderSummary
        {
            get => orderSummaryField;
            set => orderSummaryField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd", IsNullable = false)]
    public class orderHeader
    {
        private orderHeaderCarrier carrierField;

        private DateTime dateField;

        private DateTime dateFromField;

        private DateTime dateToField;

        private orderHeaderEvidentiaryResourcesMOSS evidentiaryResourcesMOSSField;

        private bool histRateField;
        private ushort idField;

        private bool isDeliveredField;

        private bool isExecutedField;

        private bool isReservedField;

        private bool markRecordField;

        private orderHeaderMOSS mOSSField;

        private orderHeaderMyIdentity myIdentityField;

        private orderHeaderNumber numberField;

        private string numberOrderField;

        private string orderTypeField;

        private orderHeaderPartnerIdentity partnerIdentityField;

        private orderHeaderPaymentType paymentTypeField;

        private bool permanentDocumentField;

        private orderHeaderPriceLevel priceLevelField;

        private string textField;

        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        public string orderType
        {
            get => orderTypeField;
            set => orderTypeField = value;
        }

        public orderHeaderNumber number
        {
            get => numberField;
            set => numberField = value;
        }

        public string numberOrder
        {
            get => numberOrderField;
            set => numberOrderField = value;
        }

        [XmlElement(DataType = "date")]
        public DateTime date
        {
            get => dateField;
            set => dateField = value;
        }

        [XmlElement(DataType = "date")]
        public DateTime dateFrom
        {
            get => dateFromField;
            set => dateFromField = value;
        }

        [XmlElement(DataType = "date")]
        public DateTime dateTo
        {
            get => dateToField;
            set => dateToField = value;
        }

        public string text
        {
            get => textField;
            set => textField = value;
        }

        public orderHeaderPartnerIdentity partnerIdentity
        {
            get => partnerIdentityField;
            set => partnerIdentityField = value;
        }

        public orderHeaderMyIdentity myIdentity
        {
            get => myIdentityField;
            set => myIdentityField = value;
        }

        public orderHeaderPaymentType paymentType
        {
            get => paymentTypeField;
            set => paymentTypeField = value;
        }

        public orderHeaderPriceLevel priceLevel
        {
            get => priceLevelField;
            set => priceLevelField = value;
        }

        public bool isExecuted
        {
            get => isExecutedField;
            set => isExecutedField = value;
        }

        public bool isDelivered
        {
            get => isDeliveredField;
            set => isDeliveredField = value;
        }

        public bool isReserved
        {
            get => isReservedField;
            set => isReservedField = value;
        }

        public orderHeaderMOSS MOSS
        {
            get => mOSSField;
            set => mOSSField = value;
        }

        public orderHeaderEvidentiaryResourcesMOSS evidentiaryResourcesMOSS
        {
            get => evidentiaryResourcesMOSSField;
            set => evidentiaryResourcesMOSSField = value;
        }

        public orderHeaderCarrier carrier
        {
            get => carrierField;
            set => carrierField = value;
        }

        public bool permanentDocument
        {
            get => permanentDocumentField;
            set => permanentDocumentField = value;
        }

        public bool histRate
        {
            get => histRateField;
            set => histRateField = value;
        }

        public bool markRecord
        {
            get => markRecordField;
            set => markRecordField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderNumber
    {
        private uint numberRequestedField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public uint numberRequested
        {
            get => numberRequestedField;
            set => numberRequestedField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderPartnerIdentity
    {
        private address addressField;

        private shipToAddress shipToAddressField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public address address
        {
            get => addressField;
            set => addressField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public shipToAddress shipToAddress
        {
            get => shipToAddressField;
            set => shipToAddressField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd", IsNullable = false)]
    public class address
    {
        private string cityField;
        private string companyField;

        private addressCountry countryField;

        private uint dicField;

        private bool dicFieldSpecified;

        private string emailField;

        private string icDphField;

        private uint icoField;

        private string mobilPhoneField;

        private string nameField;

        private string numberField;

        private string streetField;

        private string zipField;

        public string company
        {
            get => companyField;
            set => companyField = value;
        }

        public string name
        {
            get => nameField;
            set => nameField = value;
        }

        public string city
        {
            get => cityField;
            set => cityField = value;
        }

        public string street
        {
            get => streetField;
            set => streetField = value;
        }

        public string number
        {
            get => numberField;
            set => numberField = value;
        }

        public string zip
        {
            get => zipField;
            set => zipField = value;
        }

        public uint ico
        {
            get => icoField;
            set => icoField = value;
        }

        public uint dic
        {
            get => dicField;
            set => dicField = value;
        }

        [XmlIgnore]
        public bool dicSpecified
        {
            get => dicFieldSpecified;
            set => dicFieldSpecified = value;
        }

        public string icDph
        {
            get => icDphField;
            set => icDphField = value;
        }

        public addressCountry country
        {
            get => countryField;
            set => countryField = value;
        }

        public string mobilPhone
        {
            get => mobilPhoneField;
            set => mobilPhoneField = value;
        }

        public string email
        {
            get => emailField;
            set => emailField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    public class addressCountry
    {
        private byte idField;

        private string idsField;

        public byte id
        {
            get => idField;
            set => idField = value;
        }

        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd", IsNullable = false)]
    public class shipToAddress
    {
        private object cityField;
        private object companyField;

        private object emailField;

        private object nameField;

        private object streetField;

        public object company
        {
            get => companyField;
            set => companyField = value;
        }

        public object name
        {
            get => nameField;
            set => nameField = value;
        }

        public object city
        {
            get => cityField;
            set => cityField = value;
        }

        public object street
        {
            get => streetField;
            set => streetField = value;
        }

        public object email
        {
            get => emailField;
            set => emailField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderMyIdentity
    {
        private address addressField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public address address
        {
            get => addressField;
            set => addressField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderPaymentType
    {
        private byte idField;

        private string idsField;

        private string paymentTypeField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte id
        {
            get => idField;
            set => idField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string paymentType
        {
            get => paymentTypeField;
            set => paymentTypeField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderPriceLevel
    {
        private byte idField;

        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte id
        {
            get => idField;
            set => idField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderMOSS
    {
        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderEvidentiaryResourcesMOSS
    {
        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderHeaderCarrier
    {
        private byte idField;

        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte id
        {
            get => idField;
            set => idField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderDetailOrderItem
    {
        private uint codeField;

        private bool codeFieldSpecified;

        private decimal coefficientField;

        private decimal deliveredField;

        private decimal discountPercentageField;

        private orderDetailOrderItemHomeCurrency homeCurrencyField;
        private ushort idField;

        private bool payVATField;

        private bool pDPField;

        private decimal percentVATField;

        private bool percentVATFieldSpecified;

        private decimal quantityField;

        private string rateVATField;

        private orderDetailOrderItemStockItem stockItemField;

        private string textField;

        private orderDetailOrderItemTypeServiceMOSS typeServiceMOSSField;

        private string unitField;

        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        public string text
        {
            get => textField;
            set => textField = value;
        }

        public decimal quantity
        {
            get => quantityField;
            set => quantityField = value;
        }

        public decimal delivered
        {
            get => deliveredField;
            set => deliveredField = value;
        }

        public string unit
        {
            get => unitField;
            set => unitField = value;
        }

        public decimal coefficient
        {
            get => coefficientField;
            set => coefficientField = value;
        }

        public bool payVAT
        {
            get => payVATField;
            set => payVATField = value;
        }

        public string rateVAT
        {
            get => rateVATField;
            set => rateVATField = value;
        }

        public decimal percentVAT
        {
            get => percentVATField;
            set => percentVATField = value;
        }

        [XmlIgnore]
        public bool percentVATSpecified
        {
            get => percentVATFieldSpecified;
            set => percentVATFieldSpecified = value;
        }

        public decimal discountPercentage
        {
            get => discountPercentageField;
            set => discountPercentageField = value;
        }

        public orderDetailOrderItemHomeCurrency homeCurrency
        {
            get => homeCurrencyField;
            set => homeCurrencyField = value;
        }

        public orderDetailOrderItemTypeServiceMOSS typeServiceMOSS
        {
            get => typeServiceMOSSField;
            set => typeServiceMOSSField = value;
        }

        public uint code
        {
            get => codeField;
            set => codeField = value;
        }

        [XmlIgnore]
        public bool codeSpecified
        {
            get => codeFieldSpecified;
            set => codeFieldSpecified = value;
        }

        public orderDetailOrderItemStockItem stockItem
        {
            get => stockItemField;
            set => stockItemField = value;
        }

        public bool PDP
        {
            get => pDPField;
            set => pDPField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderDetailOrderItemHomeCurrency
    {
        private decimal priceField;

        private decimal priceSumField;

        private decimal priceVATField;
        private decimal unitPriceField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal unitPrice
        {
            get => unitPriceField;
            set => unitPriceField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal price
        {
            get => priceField;
            set => priceField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal priceVAT
        {
            get => priceVATField;
            set => priceVATField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal priceSum
        {
            get => priceSumField;
            set => priceSumField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderDetailOrderItemTypeServiceMOSS
    {
        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderDetailOrderItemStockItem
    {
        private stockItem stockItemField;
        private store storeField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public store store
        {
            get => storeField;
            set => storeField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public stockItem stockItem
        {
            get => stockItemField;
            set => stockItemField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd", IsNullable = false)]
    public class store
    {
        private byte idField;

        private string idsField;

        public byte id
        {
            get => idField;
            set => idField = value;
        }

        public string ids
        {
            get => idsField;
            set => idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd", IsNullable = false)]
    public class stockItem
    {
        private ulong eANField;
        private ushort idField;

        private uint idsField;

        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        public uint ids
        {
            get => idsField;
            set => idsField = value;
        }

        public ulong EAN
        {
            get => eANField;
            set => eANField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd", IsNullable = false)]
    public class orderSummary
    {
        private orderSummaryHomeCurrency homeCurrencyField;
        private string roundingDocumentField;

        private string roundingVATField;

        public string roundingDocument
        {
            get => roundingDocumentField;
            set => roundingDocumentField = value;
        }

        public string roundingVAT
        {
            get => roundingVATField;
            set => roundingVATField = value;
        }

        public orderSummaryHomeCurrency homeCurrency
        {
            get => homeCurrencyField;
            set => homeCurrencyField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    public class orderSummaryHomeCurrency
    {
        private byte price3Field;

        private byte price3SumField;

        private byte price3VATField;

        private decimal priceHighField;

        private decimal priceHighSumField;

        private decimal priceHighVATField;

        private byte priceLowField;

        private byte priceLowSumField;

        private byte priceLowVATField;
        private byte priceNoneField;

        private round roundField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte priceNone
        {
            get => priceNoneField;
            set => priceNoneField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte priceLow
        {
            get => priceLowField;
            set => priceLowField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte priceLowVAT
        {
            get => priceLowVATField;
            set => priceLowVATField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte priceLowSum
        {
            get => priceLowSumField;
            set => priceLowSumField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal priceHigh
        {
            get => priceHighField;
            set => priceHighField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal priceHighVAT
        {
            get => priceHighVATField;
            set => priceHighVATField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal priceHighSum
        {
            get => priceHighSumField;
            set => priceHighSumField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte price3
        {
            get => price3Field;
            set => price3Field = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte price3VAT
        {
            get => price3VATField;
            set => price3VATField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte price3Sum
        {
            get => price3SumField;
            set => price3SumField = value;
        }

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public round round
        {
            get => roundField;
            set => roundField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd", IsNullable = false)]
    public class round
    {
        private byte priceRoundField;

        public byte priceRound
        {
            get => priceRoundField;
            set => priceRoundField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/order.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/order.xsd", IsNullable = false)]
    public class orderDetail
    {
        private orderDetailOrderItem[] orderItemField;

        [XmlElement("orderItem")]
        public orderDetailOrderItem[] orderItem
        {
            get => orderItemField;
            set => orderItemField = value;
        }
    }
}