using Microsoft.AspNetCore.Mvc;
using PaymentAndNotifications.Models;
using PaymentAndNotifications.Services;

namespace PaymentAndNotifications.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notification
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        // POST: api/Notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _notificationService.CreateNotificationAsync(notification);
            return Ok(new { Message = "Notification created successfully." });
        }

        // PUT: api/Notification/MarkAsRead
        [HttpPut("MarkAsRead/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var result = await _notificationService.MarkAsReadAsync(notificationId);
            if (!result)
                return NotFound(new { Message = "Notification not found." });

            return Ok(new { Message = "Notification marked as read." });
        }
    }
}