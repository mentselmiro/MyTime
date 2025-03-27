using Microsoft.AspNetCore.Mvc;

namespace MyTime.MailModel;

[ApiController]
[Route("[controller]")]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(MailRequest mailRequest)
    {
        await _mailService.SendEmailAsync(mailRequest);
        return Ok("Email sent successfully");
    }
}
