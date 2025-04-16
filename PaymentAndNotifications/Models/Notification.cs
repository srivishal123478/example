namespace PaymentAndNotifications.Models
{
    public class Notification
    {
        public int NotificationID { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key (if you have a User table)
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g., "Info", "Warning", "Error"
        public string Status { get; set; } = "Unread"; // Default: "Unread", Updated to "Read" later
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}