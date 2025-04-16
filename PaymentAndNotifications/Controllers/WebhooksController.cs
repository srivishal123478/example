using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace PaymentAndNotifications.Controllers
{
    [ApiController]
    [Route("api/webhooks")]
    public class WebhooksController : ControllerBase
    {
        private readonly string _webhookSecret = "whsec_55fef11706b62ab84914b7e43b79691dbc27579c5c9cd39e43fd67d2e2e37d66";

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _webhookSecret
                );

                // Handle the event  
                if (stripeEvent.Type == Events.CheckoutSessionCompleted) // Fixed 'Events' namespace issue
                {
                    var session = stripeEvent.Data.Object as Stripe.BillingPortal.Session; // Fixed 'Session' namespace issue
                    // Add logic to handle the session if needed  
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest(); // Removed unused variable 'e'
            }
        }
    }

    internal class Events
    {
        internal static readonly string CheckoutSessionCompleted;
    }
}
