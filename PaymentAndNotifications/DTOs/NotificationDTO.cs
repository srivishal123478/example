namespace PaymentAndNotifications.DTOs
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CreateNotificationDto
    {
        public int UserID { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public class UpdateNotificationDto
    {
        public int NotificationID { get; set; }
        public string Status { get; set; }
    }
}