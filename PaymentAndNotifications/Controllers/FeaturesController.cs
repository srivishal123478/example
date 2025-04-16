using Microsoft.AspNetCore.Mvc;
using PaymentAndNotifications.Services;
using Microsoft.AspNetCore.SignalR;
using PaymentAndNotifications.Hubs;
using PaymentAndNotifications.DTOs;

namespace PaymentAndNotifications.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly NotificationService _notificationService;
        private readonly PaymentService _paymentService;
        private readonly EmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FeaturesController(
            NotificationService notificationService,
            PaymentService paymentService,
            EmailService emailService,
            IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _paymentService = paymentService;
            _emailService = emailService;
            _hubContext = hubContext;
        }

        // Display the main feature page
        public IActionResult Index()
        {
            return View();
        }

        // Notifications
        public IActionResult Notifications()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendNotification(string message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            ViewBag.Message = "Notification sent!";
            return View("Notifications");
        }

        // Payments
        public IActionResult Payments()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment()
        {
            var session = await _paymentService.CreateCheckoutSessionAsync("usd");
            return Redirect(session.Url); // Redirect to the Stripe Checkout URL
        }

        // Emails
        public IActionResult Emails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string recipientEmail, string subject, string body)
        {
            var emailRequest = new EmailRequest
            {
                RecipientName = "User",
                RecipientEmail = recipientEmail,
                Subject = subject,
                Body = body
            };

            var result = await _emailService.SendEmailAsync(emailRequest);
            ViewBag.Message = result ? "Email sent successfully!" : "Failed to send email.";
            return View("Emails");
        }
    }
}