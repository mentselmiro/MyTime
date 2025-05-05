namespace MyTime.MailModel;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
