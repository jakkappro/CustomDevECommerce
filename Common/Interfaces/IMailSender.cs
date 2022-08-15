using Common.Services.MailService;

namespace Common.Interfaces;

public interface IMailSender
{
    void SendMail(string body);
    void SendMail();
    void AddRowFromTemplate(OrderData orderData);
    void LoadTemplatesFromFile(string messageTemplatePath, string rowTemplatePath, string itemTemplatePath);
    void AddAttachment(string attachment);
    void LoadTemplatesFromFile();
    void Initialize(string from, string to, string cc, string password);
}