using System.Globalization;
using System.Net;
using System.Net.Mail;
using Common.Interfaces;

namespace Common.Services.MailService;

public class MailSender : IMailSender
{
    private readonly string _cc;
    private readonly string _from;
    private readonly string _password;
    private readonly string _to;

    private string messageTemplate;
    private string rowTemplate;
    private string itemTemplate;

    private string emailRows;

    public MailSender(string from, string to, string cc, string password)
    {
        _from = from;
        _to = to;
        _cc = cc;
        _password = password;
    }


    public void LoadTemplatesFromFile(string messageTemplatePath, string rowTemplatePath, string itemTemplatePath)
    {
        if (File.Exists(messageTemplatePath))
        {
            messageTemplate = File.ReadAllText(messageTemplatePath);
        }

        if (File.Exists(rowTemplatePath))
        {
            rowTemplate = File.ReadAllText(rowTemplatePath);
        }

        if (File.Exists(itemTemplatePath))
        {
            itemTemplate = File.ReadAllText(itemTemplatePath);
        }
    }

    public void LoadTemplatesFromFile()
    {
        if (File.Exists("./Templates/messageTemplate.html"))
        {
            messageTemplate = File.ReadAllText("./Templates/messageTemplate.html");
        }

        if (File.Exists("./Templates/itemTemplate.html"))
        {
            rowTemplate = File.ReadAllText("./Templates/itemTemplate.html");
        }

        if (File.Exists("./Templates/rowTemplate.html"))
        {
            itemTemplate = File.ReadAllText("./Templates/rowTemplate.html");
        }
    }

    public void AddRowFromTemplate(Order order)
    {
        emailRows += rowTemplate;
        emailRows = emailRows.Replace("[[OrderId]]", order.OrderId);
        emailRows = emailRows.Replace("[[purchaseDate]]", order.PurchaseDate);
        emailRows = emailRows.Replace("[[latestShipDate]]", order.PurchaseDate);
        emailRows = emailRows.Replace("[[totalPrice]]", order.TotalPrice);
        emailRows = emailRows.Replace("[[companyName]]", order.CompanyName);
        emailRows = emailRows.Replace("[[name]]", order.Name);
        emailRows = emailRows.Replace("[[address]]", order.Address);
        emailRows = emailRows.Replace("[[city]]", order.City);
        emailRows = emailRows.Replace("[[zip]]", order.Zip);
        emailRows = emailRows.Replace("[[country]]", order.Country);
        emailRows = emailRows.Replace("[[PohodaId", order.PohodaId);

        var data = "";

        foreach (var item in order.Items)
        {
            var itemData = itemTemplate;
            itemData = itemData.Replace("[[ID]]", item.ItemId);
            itemData = itemData.Replace("[[EAN]]", item.Ean);
            itemData = itemData.Replace("[[URL]]", item.Url);
            itemData = itemData.Replace("[[DEALER]]", item.Dealer);
            data += itemData;
        }

        emailRows = emailRows.Replace("[[items]]", data);
    }

    public void SendMail(string body)
    {
        try
        {
            var message = new MailMessage();
            var smtp = new SmtpClient();
            message.From = new MailAddress(_from);
            message.To.Add(new MailAddress(_to));
            message.CC.Add(new MailAddress(_cc));
            message.Subject = "Expando - Amazon objednavky za den [Today-1]";
            message.IsBodyHtml = true;
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = "mail.hostmaster.sk";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_from, _password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
        catch (Exception)
        {
            Console.WriteLine("Error while sending mail :(");
        }
    }

    public void SendMail()
    {
       SendMail(messageTemplate.Replace("[[items]]", emailRows));
    }
}