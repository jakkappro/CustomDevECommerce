#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PohodaConnector.DTO.UpdateStock;

public class UpdateStockRequest
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/data.xsd", IsNullable = false)]
    public class dataPack
    {
        private dataPackDataPackItem _dataPackItemField;

        private string _idField;

        private uint _icoField;

        private string _applicationField;

        private decimal _versionField;

        private string _noteField;

        public dataPackDataPackItem dataPackItem
        {
            get => _dataPackItemField;
            set => _dataPackItemField = value;
        }

        [XmlAttribute]
        public string id
        {
            get => _idField;
            set => _idField = value;
        }

        [XmlAttribute]
        public uint ico
        {
            get => _icoField;
            set => _icoField = value;
        }

        [XmlAttribute]
        public string application
        {
            get => _applicationField;
            set => _applicationField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => _versionField;
            set => _versionField = value;
        }

        [XmlAttribute]
        public string note
        {
            get => _noteField;
            set => _noteField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    public class dataPackDataPackItem
    {
        private stock _stockField;

        private string _idField;

        private decimal _versionField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
        public stock stock
        {
            get => _stockField;
            set => _stockField = value;
        }

        [XmlAttribute]
        public string id
        {
            get => _idField;
            set => _idField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => _versionField;
            set => _versionField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd", IsNullable = false)]
    public class stock
    {
        private stockActionType _actionTypeField;

        private stockStockHeader _stockHeaderField;

        private decimal _versionField;

        public stockActionType actionType
        {
            get => _actionTypeField;
            set => _actionTypeField = value;
        }

        public stockStockHeader stockHeader
        {
            get => _stockHeaderField;
            set => _stockHeaderField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => _versionField;
            set => _versionField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockActionType
    {
        private stockActionTypeUpdate _updateField;

        public stockActionTypeUpdate update
        {
            get => _updateField;
            set => _updateField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockActionTypeUpdate
    {
        private filter _filterField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd")]
        public filter filter
        {
            get => _filterField;
            set => _filterField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd", IsNullable = false)]
    public class filter
    {
        private ushort _codeField;

        public ushort code
        {
            get => _codeField;
            set => _codeField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeader
    {
        private string _stockTypeField;

        private ushort _codeField;

        private ulong _eAnField;

        private byte _pLuField;

        private bool _isSalesField;

        private bool _isInternetField;

        private bool _isBatchField;

        private string _purchasingRateVatField;

        private string _sellingRateVatField;

        private string _nameField;

        private string _nameComplementField;

        private string _unitField;

        private stockStockHeaderStorage _storageField;

        private StockStockHeaderTypePrice _typePriceField;

        private decimal _purchasingPriceField;

        private decimal _sellingPriceField;

        private byte _limitMinField;

        private byte _limitMaxField;

        private decimal _massField;

        private stockStockHeaderSupplier _supplierField;

        private string _producerField;

        private string _descriptionField;

        private stockStockHeaderPictures _picturesField;

        private string _noteField;

        private stockStockHeaderRelatedLinks _relatedLinksField;

        private ushort _idField;

        public string stockType
        {
            get => _stockTypeField;
            set => _stockTypeField = value;
        }

        public ushort code
        {
            get => _codeField;
            set => _codeField = value;
        }

        public ulong EAN
        {
            get => _eAnField;
            set => _eAnField = value;
        }

        public byte PLU
        {
            get => _pLuField;
            set => _pLuField = value;
        }

        public bool isSales
        {
            get => _isSalesField;
            set => _isSalesField = value;
        }

        public bool isInternet
        {
            get => _isInternetField;
            set => _isInternetField = value;
        }

        public bool isBatch
        {
            get => _isBatchField;
            set => _isBatchField = value;
        }

        public string purchasingRateVAT
        {
            get => _purchasingRateVatField;
            set => _purchasingRateVatField = value;
        }

        public string sellingRateVAT
        {
            get => _sellingRateVatField;
            set => _sellingRateVatField = value;
        }

        public string name
        {
            get => _nameField;
            set => _nameField = value;
        }

        public string nameComplement
        {
            get => _nameComplementField;
            set => _nameComplementField = value;
        }

        public string unit
        {
            get => _unitField;
            set => _unitField = value;
        }

        public stockStockHeaderStorage storage
        {
            get => _storageField;
            set => _storageField = value;
        }

        public StockStockHeaderTypePrice TypePrice
        {
            get => _typePriceField;
            set => _typePriceField = value;
        }

        public decimal purchasingPrice
        {
            get => _purchasingPriceField;
            set => _purchasingPriceField = value;
        }

        public decimal sellingPrice
        {
            get => _sellingPriceField;
            set => _sellingPriceField = value;
        }

        public byte limitMin
        {
            get => _limitMinField;
            set => _limitMinField = value;
        }

        public byte limitMax
        {
            get => _limitMaxField;
            set => _limitMaxField = value;
        }

        public decimal mass
        {
            get => _massField;
            set => _massField = value;
        }

        public stockStockHeaderSupplier supplier
        {
            get => _supplierField;
            set => _supplierField = value;
        }

        public string producer
        {
            get => _producerField;
            set => _producerField = value;
        }

        public string description
        {
            get => _descriptionField;
            set => _descriptionField = value;
        }

        public stockStockHeaderPictures pictures
        {
            get => _picturesField;
            set => _picturesField = value;
        }

        public string note
        {
            get => _noteField;
            set => _noteField = value;
        }

        public stockStockHeaderRelatedLinks relatedLinks
        {
            get => _relatedLinksField;
            set => _relatedLinksField = value;
        }

        public ushort id
        {
            get => _idField;
            set => _idField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderStorage
    {
        private string _idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => _idsField;
            set => _idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class StockStockHeaderTypePrice
    {
        private string _idsField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public string ids
        {
            get => _idsField;
            set => _idsField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderSupplier
    {
        private byte _idField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public byte id
        {
            get => _idField;
            set => _idField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderPictures
    {
        private stockStockHeaderPicturesPicture _pictureField;

        public stockStockHeaderPicturesPicture picture
        {
            get => _pictureField;
            set => _pictureField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderPicturesPicture
    {
        private string _filepathField;

        private string _descriptionField;

        private bool _defaultField;

        public string filepath
        {
            get => _filepathField;
            set => _filepathField = value;
        }

        public string description
        {
            get => _descriptionField;
            set => _descriptionField = value;
        }

        [XmlAttribute]
        public bool @default
        {
            get => _defaultField;
            set => _defaultField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderRelatedLinks
    {
        private stockStockHeaderRelatedLinksRelatedLink _relatedLinkField;

        public stockStockHeaderRelatedLinksRelatedLink relatedLink
        {
            get => _relatedLinkField;
            set => _relatedLinkField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/stock.xsd")]
    public class stockStockHeaderRelatedLinksRelatedLink
    {
        private string _addressUrlField;

        private string _descriptionField;

        private byte _orderField;

        public string addressURL
        {
            get => _addressUrlField;
            set => _addressUrlField = value;
        }

        public string description
        {
            get => _descriptionField;
            set => _descriptionField = value;
        }

        public byte order
        {
            get => _orderField;
            set => _orderField = value;
        }
    }
}