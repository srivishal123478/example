namespace PaymentAndNotifications.DTOs
{
    public class EmailRequest
    {
        public int UserId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? HtmlContent { get; set; } 
    }

    public class EmailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}