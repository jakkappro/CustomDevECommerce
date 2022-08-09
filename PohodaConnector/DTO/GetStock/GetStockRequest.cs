#pragma warning disable CS8618
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Xml.Serialization;

namespace PohodaConnector.DTO.GetStock;

public class GetStockRequest
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/data.xsd", IsNullable = false)]
    public class dataPack
    {

        private dataPackDataPackItem dataPackItemField;

        private string idField;

        private uint icoField;

        private string applicationField;

        private decimal versionField;

        private string noteField;

        public dataPackDataPackItem dataPackItem
        {
            get => dataPackItemField;
            set => dataPackItemField = value;
        }

        [XmlAttribute]
        public string id
        {
            get => idField;
            set => idField = value;
        }

        [XmlAttribute]
        public uint ico
        {
            get => icoField;
            set => icoField = value;
        }

        [XmlAttribute]
        public string application
        {
            get => applicationField;
            set => applicationField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    public class dataPackDataPackItem
    {

        private listStockRequest listStockRequestField;

        private string idField;

        private decimal versionField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
        public listStockRequest listStockRequest
        {
            get => listStockRequestField;
            set => listStockRequestField = value;
        }

        [XmlAttribute]
        public string id
        {
            get => idField;
            set => idField = value;
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
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd", IsNullable = false)]
    public class listStockRequest
    {

        private listStockRequestRequestStock? requestStockField;

        private decimal versionField;

        private decimal stockVersionField;

        public listStockRequestRequestStock requestStock
        {
            get => requestStockField;
            set => requestStockField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }

        [XmlAttribute]
        public decimal stockVersion
        {
            get => stockVersionField;
            set => stockVersionField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/list_stock.xsd")]
    public class listStockRequestRequestStock
    {

        private filter filterField;

        [XmlElement(Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd")]
        public filter filter
        {
            get => filterField;
            set => filterField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd")]
    [XmlRoot(Namespace = "http://www.stormware.cz/schema/version_2/filter.xsd", IsNullable = false)]
    public class filter
    {

        private string codeField;

        public string code
        {
            get => codeField;
            set => codeField = value;
        }
    }


}