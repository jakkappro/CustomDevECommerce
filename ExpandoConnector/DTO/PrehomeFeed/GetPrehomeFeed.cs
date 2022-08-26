#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace ExpandoConnector.DTO.PrehomeFeed;

public class GetPrehomeFeed
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SHOP
    {
        private SHOPSHOPITEM[] sHOPITEMField;

        [XmlElement("SHOPITEM")]
        public SHOPSHOPITEM[] SHOPITEM
        {
            get => sHOPITEMField;
            set => sHOPITEMField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SHOPSHOPITEM
    {
        private string cATEGORYTEXTField;

        private string dEALERField;

        private byte dELIVERY_DATEField;

        private SHOPSHOPITEMDELIVERY dELIVERYField;

        private string dESCRIPTIONField;

        private string eANField;

        private string iMGURLField;
        private uint iTEM_IDField;

        private string mANUFACTURERField;

        private decimal pRICE_VATField;

        private decimal pRICEField;

        private string pRODUCT_CODEField;

        private string pRODUCTField;

        private string pRODUCTNAMEField;

        private uint pRODUCTNOField;

        private byte sTOCKField;

        private string uRLField;

        private byte vATField;

        private string wEIGHT_UNITSField;

        private decimal wEIGHTField;

        public uint ITEM_ID
        {
            get => iTEM_IDField;
            set => iTEM_IDField = value;
        }

        public string PRODUCTNAME
        {
            get => pRODUCTNAMEField;
            set => pRODUCTNAMEField = value;
        }

        public string PRODUCT
        {
            get => pRODUCTField;
            set => pRODUCTField = value;
        }

        public string PRODUCT_CODE
        {
            get => pRODUCT_CODEField;
            set => pRODUCT_CODEField = value;
        }

        public string DESCRIPTION
        {
            get => dESCRIPTIONField;
            set => dESCRIPTIONField = value;
        }

        public string URL
        {
            get => uRLField;
            set => uRLField = value;
        }

        public string IMGURL
        {
            get => iMGURLField;
            set => iMGURLField = value;
        }

        public decimal PRICE
        {
            get => pRICEField;
            set => pRICEField = value;
        }

        public decimal PRICE_VAT
        {
            get => pRICE_VATField;
            set => pRICE_VATField = value;
        }

        public byte VAT
        {
            get => vATField;
            set => vATField = value;
        }

        public string MANUFACTURER
        {
            get => mANUFACTURERField;
            set => mANUFACTURERField = value;
        }

        public string DEALER
        {
            get => dEALERField;
            set => dEALERField = value;
        }

        public string CATEGORYTEXT
        {
            get => cATEGORYTEXTField;
            set => cATEGORYTEXTField = value;
        }

        public string EAN
        {
            get => eANField;
            set => eANField = value;
        }

        public uint PRODUCTNO
        {
            get => pRODUCTNOField;
            set => pRODUCTNOField = value;
        }

        public byte DELIVERY_DATE
        {
            get => dELIVERY_DATEField;
            set => dELIVERY_DATEField = value;
        }

        public SHOPSHOPITEMDELIVERY DELIVERY
        {
            get => dELIVERYField;
            set => dELIVERYField = value;
        }

        public decimal WEIGHT
        {
            get => wEIGHTField;
            set => wEIGHTField = value;
        }

        public string WEIGHT_UNITS
        {
            get => wEIGHT_UNITSField;
            set => wEIGHT_UNITSField = value;
        }

        public byte STOCK
        {
            get => sTOCKField;
            set => sTOCKField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SHOPSHOPITEMDELIVERY
    {
        private string dELIVERY_IDField;

        private decimal dELIVERY_PRICE_CODField;

        private decimal dELIVERY_PRICEField;

        public string DELIVERY_ID
        {
            get => dELIVERY_IDField;
            set => dELIVERY_IDField = value;
        }

        public decimal DELIVERY_PRICE
        {
            get => dELIVERY_PRICEField;
            set => dELIVERY_PRICEField = value;
        }

        public decimal DELIVERY_PRICE_COD
        {
            get => dELIVERY_PRICE_CODField;
            set => dELIVERY_PRICE_CODField = value;
        }
    }
}