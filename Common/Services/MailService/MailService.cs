using System.Net;
using System.Net.Mail;

namespace Common.Services.MailService;

public class MailService
{
    private readonly string _cc;
    private readonly string _from;
    private readonly string _password;
    private readonly string _to;

    public MailService(string from, string to, string cc, string password)
    {
        _from = from; 
        _to = to;
        _cc = cc;
        _password = password;
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
}