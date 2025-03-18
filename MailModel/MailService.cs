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
        MimeMessage email = new()
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail)
        };
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;

        BodyBuilder builder = new()
        {
            HtmlBody = mailRequest.Body
        };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
