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
        _cc = "";
        _from = "";
        _password = "";
        _to = "";
        _messageTemplate = "";
        _rowTemplate = "";
        _itemTemplate = "";
        _emailRows = "";
    }

    public void Initialize(string from, string to, string cc, string password)
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
        {
            _messageTemplate = File.ReadAllText("./Templates/messageTemplate.html");
        }
        else
        {
            _logger.LogWarning("Couldn't load message template");

        }

        if (File.Exists("./Templates/itemTemplate.html"))
        {
            _rowTemplate = File.ReadAllText("./Templates/rowTemplate.html");
        }
        else
        {
            _logger.LogWarning("Couldn't load item template");

        }

        if (File.Exists("./Templates/rowTemplate.html"))
        {
            _itemTemplate = File.ReadAllText("./Templates/itemTemplate.html");
        }
        else
        {
            _logger.LogWarning("Couldn't load row template");

        }
    }

    public void AddRowFromTemplate(OrderData orderData)
    {
        _emailRows += _rowTemplate;
        _emailRows = _emailRows.Replace("[[OrderId]]", orderData.OrderId);
        _emailRows = _emailRows.Replace("[[purchaseDate]]", orderData.PurchaseDate);
        _emailRows = _emailRows.Replace("[[latestShipDate]]", orderData.PurchaseDate);
        _emailRows = _emailRows.Replace("[[totalPrice]]", orderData.TotalPrice);
        _emailRows = _emailRows.Replace("[[companyName]]", orderData.CompanyName);
        _emailRows = _emailRows.Replace("[[name]]", orderData.Name);
        _emailRows = _emailRows.Replace("[[address]]", orderData.Address);
        _emailRows = _emailRows.Replace("[[city]]", orderData.City);
        _emailRows = _emailRows.Replace("[[zip]]", orderData.Zip);
        _emailRows = _emailRows.Replace("[[country]]", orderData.Country);
        _emailRows = _emailRows.Replace("[[PohodaId]]", orderData.PohodaId);

        var data = "";

        foreach (var item in orderData.Items)
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
            message.Subject = $"Expando - Amazon objednavky za den {DateTime.Now.AddDays(-1).ToShortDateString()}";
            message.IsBodyHtml = true;
            message.Body = body;

            // for sending attachments
            // var attachment = new Attachment("c:/textfile.txt");
            // message.Attachments.Add(attachment);

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
        SendMail(_messageTemplate.Replace("[[data]]", _emailRows));
    }
}