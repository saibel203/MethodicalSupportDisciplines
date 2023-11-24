namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(string toEmail, string subject, string content, string? link, string? linkText);
}