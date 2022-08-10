using Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Common.Services.MailService;

public class MailSender : IMailSender
{
    private string _cc;
    private string _from;
    private string _password;
    private readonly ILogger<MailSender> _logger;
    private string _to;

    private string _messageTemplate;
    private string _rowTemplate;
    private string _itemTemplate;

    private string _emailRows;

    public MailSender(ILogger<MailSender> logger)
    {
        _logger = logger;
    }

    public void SetInformation(string from, string to, string cc, string password)
    {
        _from = from;
        _to = to;
        _cc = cc;
        _password = password;
    }

    public void LoadTemplatesFromFile(string messageTemplatePath, string rowTemplatePath, string itemTemplatePath)
    {
        if (File.Exists(messageTemplatePath)) _messageTemplate = File.ReadAllText(messageTemplatePath);

        if (File.Exists(rowTemplatePath)) _rowTemplate = File.ReadAllText(rowTemplatePath);

        if (File.Exists(itemTemplatePath)) _itemTemplate = File.ReadAllText(itemTemplatePath);
    }

    public void LoadTemplatesFromFile()
    {
        if (File.Exists("./Templates/messageTemplate.html"))
            _messageTemplate = File.ReadAllText("./Templates/messageTemplate.html");

        if (File.Exists("./Templates/itemTemplate.html"))
            _rowTemplate = File.ReadAllText("./Templates/itemTemplate.html");

        if (File.Exists("./Templates/rowTemplate.html"))
            _itemTemplate = File.ReadAllText("./Templates/rowTemplate.html");
    }

    public void AddRowFromTemplate(Order order)
    {
        _emailRows += _rowTemplate;
        _emailRows = _emailRows.Replace("[[OrderId]]", order.OrderId);
        _emailRows = _emailRows.Replace("[[purchaseDate]]", order.PurchaseDate);
        _emailRows = _emailRows.Replace("[[latestShipDate]]", order.PurchaseDate);
        _emailRows = _emailRows.Replace("[[totalPrice]]", order.TotalPrice);
        _emailRows = _emailRows.Replace("[[companyName]]", order.CompanyName);
        _emailRows = _emailRows.Replace("[[name]]", order.Name);
        _emailRows = _emailRows.Replace("[[address]]", order.Address);
        _emailRows = _emailRows.Replace("[[city]]", order.City);
        _emailRows = _emailRows.Replace("[[zip]]", order.Zip);
        _emailRows = _emailRows.Replace("[[country]]", order.Country);
        _emailRows = _emailRows.Replace("[[PohodaId", order.PohodaId);

        var data = "";

        foreach (var item in order.Items)
        {
            var itemData = _itemTemplate;
            itemData = itemData.Replace("[[ID]]", item.ItemId);
            itemData = itemData.Replace("[[EAN]]", item.Ean);
            itemData = itemData.Replace("[[URL]]", item.Url);
            itemData = itemData.Replace("[[DEALER]]", item.Dealer);
            data += itemData;
        }

        _emailRows = _emailRows.Replace("[[items]]", data);
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
            _logger.LogError("Couldn't send mail.");
            throw;
        }
    }

    public void SendMail()
    {
        SendMail(_messageTemplate.Replace("[[items]]", _emailRows));
    }
}