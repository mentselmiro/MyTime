using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MyTime.MailModel;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
