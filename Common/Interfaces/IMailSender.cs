using Common.Services.MailService;

namespace Common.Interfaces;

public interface IMailSender
{
    void SendMail(string body);
    void SendMail();
    void AddRowFromTemplate(Order order);
    void LoadTemplatesFromFile(string messageTemplatePath, string rowTemplatePath, string itemTemplatePath);
    void LoadTemplatesFromFile();
}