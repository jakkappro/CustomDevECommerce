using System.ComponentModel;
using System.Xml.Serialization;
#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

namespace XMLFeedSynchronizer.XMLFeeds;

public class SkHracky
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

        private string iTEMIDField;

        private string pRODUCTField;

        private string dESCRIPTIONField;

        private string iMGURLField;

        private string[] iMGURL_ALTERNATIVEField;

        private string aGEField;

        private string sIZEField;

        private SHOPSHOPITEMVARIANT[] vARIANTSField;

        private ushort sTOCKField;

        private bool sTOCKFieldSpecified;

        private byte dPHField;

        private bool dPHFieldSpecified;

        private string eANField;

        private decimal pRICEField;

        private string uRLField;

        private string mANUFACTURERField;

        private decimal pRICE_VAT_RECOMMENDField;

        private string cATEGORYTEXTField;

        private string cATEGORYTEXT_2Field;

        private string cATEGORYTEXT_3Field;

        [XmlElement("ITEM-ID")]
        public string ITEMID
        {
            get => iTEMIDField;
            set => iTEMIDField = value;
        }

        public string PRODUCT
        {
            get => pRODUCTField;
            set => pRODUCTField = value;
        }

        public string DESCRIPTION
        {
            get => dESCRIPTIONField;
            set => dESCRIPTIONField = value;
        }

        public string IMGURL
        {
            get => iMGURLField;
            set => iMGURLField = value;
        }

        [XmlElement("IMGURL_ALTERNATIVE")]
        public string[] IMGURL_ALTERNATIVE
        {
            get => iMGURL_ALTERNATIVEField;
            set => iMGURL_ALTERNATIVEField = value;
        }

        public string AGE
        {
            get => aGEField;
            set => aGEField = value;
        }

        public string SIZE
        {
            get => sIZEField;
            set => sIZEField = value;
        }

        [XmlArrayItem("VARIANT", IsNullable = false)]
        public SHOPSHOPITEMVARIANT[] VARIANTS
        {
            get => vARIANTSField;
            set => vARIANTSField = value;
        }

        public ushort STOCK
        {
            get => sTOCKField;
            set => sTOCKField = value;
        }

        [XmlIgnore]
        public bool STOCKSpecified
        {
            get => sTOCKFieldSpecified;
            set => sTOCKFieldSpecified = value;
        }

        public byte DPH
        {
            get => dPHField;
            set => dPHField = value;
        }

        [XmlIgnore]
        public bool DPHSpecified
        {
            get => dPHFieldSpecified;
            set => dPHFieldSpecified = value;
        }

        public string EAN
        {
            get => eANField;
            set => eANField = value;
        }

        public decimal PRICE
        {
            get => pRICEField;
            set => pRICEField = value;
        }

        public string URL
        {
            get => uRLField;
            set => uRLField = value;
        }

        public string MANUFACTURER
        {
            get => mANUFACTURERField;
            set => mANUFACTURERField = value;
        }

        public decimal PRICE_VAT_RECOMMEND
        {
            get => pRICE_VAT_RECOMMENDField;
            set => pRICE_VAT_RECOMMENDField = value;
        }

        public string CATEGORYTEXT
        {
            get => cATEGORYTEXTField;
            set => cATEGORYTEXTField = value;
        }

        public string CATEGORYTEXT_2
        {
            get => cATEGORYTEXT_2Field;
            set => cATEGORYTEXT_2Field = value;
        }

        public string CATEGORYTEXT_3
        {
            get => cATEGORYTEXT_3Field;
            set => cATEGORYTEXT_3Field = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SHOPSHOPITEMVARIANT
    {

        private string nAMEField;

        private string cODEField;

        private string cOLORField;

        private string sTOCKField;

        private string aVAILABLE_STRField;

        private byte aVAILABLEField;

        private decimal pRICE_VAT_RECOMMENDField;

        public string NAME
        {
            get => nAMEField;
            set => nAMEField = value;
        }

        public string CODE
        {
            get => cODEField;
            set => cODEField = value;
        }

        public string COLOR
        {
            get => cOLORField;
            set => cOLORField = value;
        }

        public string STOCK
        {
            get => sTOCKField;
            set => sTOCKField = value;
        }

        public string AVAILABLE_STR
        {
            get => aVAILABLE_STRField;
            set => aVAILABLE_STRField = value;
        }

        public byte AVAILABLE
        {
            get => aVAILABLEField;
            set => aVAILABLEField = value;
        }

        public decimal PRICE_VAT_RECOMMEND
        {
            get => pRICE_VAT_RECOMMENDField;
            set => pRICE_VAT_RECOMMENDField = value;
        }
    }
}