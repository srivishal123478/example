using Microsoft.AspNetCore.Mvc;
using PaymentAndNotifications.Services;
using PaymentAndNotifications.DTOs;

namespace PaymentAndNotifications.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        // POST: api/Email/Send
        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendEmailAsync(emailRequest);
            if (!result)
                return BadRequest(new { Message = "Failed to send email." });

            return Ok(new { Message = "Email sent successfully!" });
        }

        // GET: api/Email/Resend/{userId}
        [HttpGet("Resend/{userId}")]
        public async Task<IActionResult> ResendEmail(int userId)
        {
            var result = await _emailService.ResendEmailAsync(userId);
            if (!result)
                return NotFound(new { Message = "No emails found for this user." });

            return Ok(new { Message = "Email resent successfully!" });
        }
    }
}