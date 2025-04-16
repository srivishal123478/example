namespace PaymentAndNotifications.Models
{
    public class Payment
    {
        public int PaymentID { get; set; } // Primary Key
        public decimal Amount { get; set; } // Payment Amount
        public string Currency { get; set; } = "USD"; // Default: USD
        public string Status { get; set; } = "Pending"; // e.g., "Pending", "Completed", "Failed"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for payment creation
        public string? PaymentSessionId { get; set; } // Stripe session ID (if applicable)
    }
}