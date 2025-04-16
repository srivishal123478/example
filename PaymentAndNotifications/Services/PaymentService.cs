using Microsoft.AspNetCore.Http;
using Stripe;
using Stripe.Checkout;
using System.IO;
using System.Threading.Tasks;

public class PaymentService
{
    private readonly string _stripeWebhookSecret;

    public PaymentService(string stripeWebhookSecret)
    {
        _stripeWebhookSecret = stripeWebhookSecret;
    }

    public async Task<Session> CreateCheckoutSessionAsync(string currency)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 5000, // Amount in cents (e.g., $50.00)
                        Currency = currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Sample Product",
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = "https://yourdomain.com/success",
            CancelUrl = "https://yourdomain.com/cancel",
        };

        var service = new SessionService();
        return await service.CreateAsync(options);
    }

    public async Task<bool> HandleStripeWebhookAsync(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        try
        {
            // Construct the Stripe Event
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                request.Headers["Stripe-Signature"],
                _stripeWebhookSecret
            );

            // Handle the event based on its type
            if (stripeEvent.Type == "checkout.session.completed") // Use string literal for event type
            {
                var session = stripeEvent.Data.Object as Session;
                // Handle successful checkout session
                return true;
            }
        }
        catch (StripeException ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"Stripe Exception: {ex.Message}");
            return false;
        }

        return false;
    }
}