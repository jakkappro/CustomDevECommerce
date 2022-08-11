#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PohodaConnector.DTO.GetStock;

public class GetStockResponse
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/response.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/response.xsd", IsNullable = false)]
    public class responsePack
    {
        private responsePackResponsePackItem responsePackItemField;

        private decimal versionField;

        private string idField;

        private string stateField;

        private string programVersionField;

        private uint icoField;

        private string keyField;

        private string noteField;

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
        private listStock listStockField;

        private decimal versionField;

        private string idField;

        private string stateField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
        public listStock listStock
        {
            get => listStockField;
            set => listStockField = value;
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd", IsNullable = false)]
    public class listStock
    {
        private listStockStock stockField;

        private decimal versionField;

        private DateTime dateTimeStampField;

        private DateTime dateValidFromField;

        private string stateField;

        public listStockStock stock
        {
            get => stockField;
            set => stockField = value;
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
    public class listStockStock
    {
        private stockHeader stockHeaderField;

        private stockPriceItemStockPrice[] stockPriceItemField;

        private decimal versionField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
        public stockHeader stockHeader
        {
            get => stockHeaderField;
            set => stockHeaderField = value;
        }

        [XmlArray(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
        [XmlArrayItem("stockPrice", IsNullable = false)]
        public stockPriceItemStockPrice[] stockPriceItem
        {
            get => stockPriceItemField;
            set => stockPriceItemField = value;
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd", IsNullable = false)]
    public class stockHeader
    {
        private decimal idField;

        private string stockTypeField;

        private decimal codeField;

        private ulong eANField;

        private bool isSalesField;

        private bool isInternetField;

        private string purchasingRateVATField;

        private string sellingRateVATField;

        private string nameField;

        private string nameComplementField;

        private string unitField;

        private stockHeaderStorage storageField;

        private stockHeaderTypePrice typePriceField;

        private byte weightedPurchasePriceField;

        private stockHeaderSellingPrice sellingPriceField;

        private decimal massField;

        private decimal countField;

        private decimal countReceivedOrdersField;

        private decimal reservationField;

        private decimal reclamationField;

        private decimal serviceField;

        private stockHeaderSupplier supplierField;

        private decimal orderQuantityField;

        private decimal countIssuedOrdersField;

        private string shortNameField;

        private bool newsField;

        private bool clearanceSaleField;

        private bool saleField;

        private bool recommendedField;

        private bool discountField;

        private bool prepareField;

        private bool pDPField;

        private string descriptionField;

        private stockHeaderPictures picturesField;

        private string noteField;

        private bool markRecordField;

        private object parametersField;

        public decimal id
        {
            get => idField;
            set => idField = value;
        }

        public string stockType
        {
            get => stockTypeField;
            set => stockTypeField = value;
        }

        public decimal code
        {
            get => codeField;
            set => codeField = value;
        }

        public ulong EAN
        {
            get => eANField;
            set => eANField = value;
        }

        public bool isSales
        {
            get => isSalesField;
            set => isSalesField = value;
        }

        public bool isInternet
        {
            get => isInternetField;
            set => isInternetField = value;
        }

        public string purchasingRateVAT
        {
            get => purchasingRateVATField;
            set => purchasingRateVATField = value;
        }

        public string sellingRateVAT
        {
            get => sellingRateVATField;
            set => sellingRateVATField = value;
        }

        public string name
        {
            get => nameField;
            set => nameField = value;
        }

        public string nameComplement
        {
            get => nameComplementField;
            set => nameComplementField = value;
        }

        public string unit
        {
            get => unitField;
            set => unitField = value;
        }

        public stockHeaderStorage storage
        {
            get => storageField;
            set => storageField = value;
        }

        public stockHeaderTypePrice typePrice
        {
            get => typePriceField;
            set => typePriceField = value;
        }

        public byte weightedPurchasePrice
        {
            get => weightedPurchasePriceField;
            set => weightedPurchasePriceField = value;
        }

        public stockHeaderSellingPrice sellingPrice
        {
            get => sellingPriceField;
            set => sellingPriceField = value;
        }

        public decimal mass
        {
            get => massField;
            set => massField = value;
        }

        public decimal count
        {
            get => countField;
            set => countField = value;
        }

        public decimal countReceivedOrders
        {
            get => countReceivedOrdersField;
            set => countReceivedOrdersField = value;
        }

        public decimal reservation
        {
            get => reservationField;
            set => reservationField = value;
        }

        public decimal reclamation
        {
            get => reclamationField;
            set => reclamationField = value;
        }

        public decimal service
        {
            get => serviceField;
            set => serviceField = value;
        }

        public stockHeaderSupplier supplier
        {
            get => supplierField;
            set => supplierField = value;
        }

        public decimal orderQuantity
        {
            get => orderQuantityField;
            set => orderQuantityField = value;
        }

        public decimal countIssuedOrders
        {
            get => countIssuedOrdersField;
            set => countIssuedOrdersField = value;
        }

        public string shortName
        {
            get => shortNameField;
            set => shortNameField = value;
        }

        public bool news
        {
            get => newsField;
            set => newsField = value;
        }

        public bool clearanceSale
        {
            get => clearanceSaleField;
            set => clearanceSaleField = value;
        }

        public bool sale
        {
            get => saleField;
            set => saleField = value;
        }

        public bool recommended
        {
            get => recommendedField;
            set => recommendedField = value;
        }

        public bool discount
        {
            get => discountField;
            set => discountField = value;
        }

        public bool prepare
        {
            get => prepareField;
            set => prepareField = value;
        }

        public bool PDP
        {
            get => pDPField;
            set => pDPField = value;
        }

        public string description
        {
            get => descriptionField;
            set => descriptionField = value;
        }

        public stockHeaderPictures pictures
        {
            get => picturesField;
            set => picturesField = value;
        }

        public string note
        {
            get => noteField;
            set => noteField = value;
        }

        public bool markRecord
        {
            get => markRecordField;
            set => markRecordField = value;
        }

        public object parameters
        {
            get => parametersField;
            set => parametersField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderStorage
    {
        private decimal idField;

        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal id
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderTypePrice
    {
        private decimal idField;

        private string idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal id
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderSellingPrice
    {
        private bool payVATField;

        private decimal valueField;

        [XmlAttribute]
        public bool payVAT
        {
            get => payVATField;
            set => payVATField = value;
        }

        [XmlText]
        public decimal Value
        {
            get => valueField;
            set => valueField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderSupplier
    {
        private decimal idField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal id
        {
            get => idField;
            set => idField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderPictures
    {
        private stockHeaderPicturesPicture pictureField;

        public stockHeaderPicturesPicture picture
        {
            get => pictureField;
            set => pictureField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockHeaderPicturesPicture
    {
        private ushort idField;

        private object filepathField;

        private string descriptionField;

        private decimal orderField;

        private bool defaultField;

        public ushort id
        {
            get => idField;
            set => idField = value;
        }

        public object filepath
        {
            get => filepathField;
            set => filepathField = value;
        }

        public string description
        {
            get => descriptionField;
            set => descriptionField = value;
        }

        public decimal order
        {
            get => orderField;
            set => orderField = value;
        }

        [XmlAttribute]
        public bool @default
        {
            get => defaultField;
            set => defaultField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockPriceItemStockPrice
    {
        private decimal idField;

        private string idsField;

        private decimal priceField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public decimal id
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
        public decimal price
        {
            get => priceField;
            set => priceField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd", IsNullable = false)]
    public class stockPriceItem
    {
        private stockPriceItemStockPrice[] stockPriceField;

        [XmlElement("stockPrice")]
        public stockPriceItemStockPrice[] stockPrice
        {
            get => stockPriceField;
            set => stockPriceField = value;
        }
    }
}