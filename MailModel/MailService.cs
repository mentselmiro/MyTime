using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MyTime.MailModel;

public class MailService(IOptions<MailSettings> mailSettings) : IMailService
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.SenderEmail)
        };
        email.From.Add(MailboxAddress.Parse(_mailSettings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;

        var builder = new BodyBuilder { HtmlBody = mailRequest.Body };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
