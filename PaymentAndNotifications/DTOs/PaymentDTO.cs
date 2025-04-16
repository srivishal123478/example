namespace PaymentAndNotifications.DTOs
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PaymentSessionUrl { get; set; } // Optional for linking Stripe session
    }

    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class UpdatePaymentDto
    {
        public int PaymentID { get; set; }
        public string Status { get; set; }
    }
}