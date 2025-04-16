using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentAndNotifications.DTOs;
using PaymentAndNotifications.Models;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;
    private readonly List<EmailLog> _emailLogs; // Simulating a database of sent emails

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _emailLogs = new List<EmailLog>(); // Initialize with test data
    }

    public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
    {
        try
        {
            // Fetch email settings from configuration
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var password = _configuration["EmailSettings:Password"];
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);

            // Configure SMTP client
            using var smtpClient = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(senderEmail, password),
                EnableSsl = true
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = emailRequest.Subject,
                Body = emailRequest.Body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(emailRequest.RecipientEmail);

            // Send the email
            await smtpClient.SendMailAsync(mailMessage);

            // Log the email to in-memory list
            _emailLogs.Add(new EmailLog
            {
                UserId = emailRequest.UserId,
                Email = emailRequest.RecipientEmail,
                Subject = emailRequest.Subject,
                Body = emailRequest.Body,
                SentAt = DateTime.UtcNow
            });

            _logger.LogInformation($"Email sent to {emailRequest.RecipientEmail}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ResendEmailAsync(int userId)
    {
        try
        {
            // Locate the last email sent to the user
            var emailLog = _emailLogs.FindLast(e => e.UserId == userId);
            if (emailLog == null)
            {
                _logger.LogWarning($"No email found for userId {userId}");
                return false;
            }

            // Reuse the SendEmailAsync logic with previous email log details
            var emailRequest = new EmailRequest
            {
                UserId = emailLog.UserId,
                RecipientEmail = emailLog.Email,
                Subject = emailLog.Subject,
                Body = emailLog.Body
            };

            return await SendEmailAsync(emailRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to resend email for userId {userId}: {ex.Message}");
            return false;
        }
    }
}

internal class EmailLog
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime SentAt { get; set; }
}