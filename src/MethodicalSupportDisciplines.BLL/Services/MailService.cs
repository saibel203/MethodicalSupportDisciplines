using System.Text;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Core.IOptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MethodicalSupportDisciplines.BLL.Services;

public class MailService : IMailService
{
    private readonly SendGridOptions _sendGridOptions;
    private readonly ILogger<MailService> _logger;

    public MailService(IOptions<SendGridOptions> sendGridOptions, ILogger<MailService> logger)
    {
        _sendGridOptions = sendGridOptions.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string content, string? link, string? linkText)
    {
        try
        {
            string filePath = Path.Combine(Environment.CurrentDirectory + _sendGridOptions.EmailTemplatePath);
            StringBuilder htmlText = new();

            string htmlFile = await File.ReadAllTextAsync(filePath);
            htmlText.Append(htmlFile);

            htmlText.Replace("#ReceiverEmail#", toEmail);
            htmlText.Replace("#TopicLatter#", subject);
            htmlText.Replace("#ReceiverEmail2#", toEmail);
            htmlText.Replace("#LetterMessage#", content);

            if (link is not null && linkText is not null)
            {
                htmlText.Replace("#Link#", link);
                htmlText.Replace("#LinkText#", linkText);
            }

            string apiKey = _sendGridOptions.ApiKey;
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress(_sendGridOptions.AdminEmail, _sendGridOptions.AdminUsername);
            EmailAddress to = new EmailAddress(toEmail);
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject,
                htmlText.ToString(), htmlText.ToString());
            
            await client.SendEmailAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error send mail");
            throw;
        }
    }
}