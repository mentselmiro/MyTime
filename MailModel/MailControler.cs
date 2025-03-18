using Microsoft.AspNetCore.Mvc;

namespace MyTime.MailModel
{
    public class MailControler
    {
        [ApiController]
        [Route("[controller]")]
        public class MailController(IMailService mailService) : ControllerBase
        {
            private readonly IMailService _mailService = mailService;

            [HttpPost]
            public async Task<IActionResult> SendEmail(MailRequest mailRequest)
            {
                await _mailService.SendEmailAsync(mailRequest);
                return Ok("Email sent successfully");
            }
        }
    }
}
