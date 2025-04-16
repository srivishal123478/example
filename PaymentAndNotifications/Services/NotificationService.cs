using PaymentAndNotifications.Models;
using PaymentAndNotifications.Repositories;

namespace PaymentAndNotifications.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _repository;

        public NotificationService(NotificationRepository repository)
        {
            _repository = repository;
        }

        // Get notifications by user ID
        public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _repository.GetNotificationsByUserIdAsync(userId);
        }

        // Create a new notification
        public async Task CreateNotificationAsync(Notification notification)
        {
            notification.Timestamp = DateTime.UtcNow;
            notification.Status = "Unread"; // Default status
            await _repository.AddNotificationAsync(notification);
        }

        // Mark a notification as read
        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var notification = await _repository.GetNotificationByIdAsync(notificationId);
            if (notification == null)
                return false;

            notification.Status = "Read";
            await _repository.UpdateNotificationAsync(notification);
            return true;
        }
    }
}