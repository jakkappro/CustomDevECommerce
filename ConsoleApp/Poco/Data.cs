namespace ConsoleApp.Poco;

public class Data
{
    public int Id { get; set; }
    public string PohodaOrderId { get; set; }
    public string AmazonOrderId { get; set; }
    public string InternalPackageId { get; set; }
    public string PackageTrackingNumber { get; set; }
    public string PackageBarCode { get; set; }
    public string CarrierInfo { get; set; }
    public DateTime DateCreated { get; set; }
    public string Status { get; set; }
    public string LabelId { get; set; }

    public string MarketPlace { get; set; }
}