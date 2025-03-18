using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MyTime.MailModel
{
    public class MailSettings
    {
        public string Mail { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }

    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }

    //public class MailService(IOptions<MailSettings> mailSettings) : IMailService
    //{
    //    private readonly MailSettings _mailSettings = mailSettings.Value;

    //    public async Task SendEmailAsync(MailRequest mailRequest)
    //    {
    //        MimeMessage email = new()
    //        {
    //            Sender = MailboxAddress.Parse(_mailSettings.Mail)
    //        };
    //        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
    //        email.Subject = mailRequest.Subject;

    //        BodyBuilder builder = new()
    //        {
    //            HtmlBody = mailRequest.Body
    //        };
    //        email.Body = builder.ToMessageBody();

    //        using var smtp = new SmtpClient();
    //        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
    //        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
    //        await smtp.SendAsync(email);
    //        smtp.Disconnect(true);
    //    }
    //}

    //[ApiController]
    //[Route("[controller]")]
    //public class MailController : ControllerBase
    //{
    //    private readonly IMailService _mailService;

    //    public MailController(IMailService mailService)
    //    {
    //        _mailService = mailService;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SendEmail(MailRequest mailRequest)
    //    {
    //        await _mailService.SendEmailAsync(mailRequest);
    //        return Ok("Email sent successfully");
    //    }
    //}

}
