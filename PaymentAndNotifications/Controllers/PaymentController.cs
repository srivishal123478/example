using Microsoft.AspNetCore.Mvc;
using PaymentAndNotifications.Services;
using Stripe.Checkout;
using Stripe;

namespace PaymentAndNotifications.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST: api/Payment/Checkout
        [HttpPost("Checkout")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] string currency)
        {
            var session = await _paymentService.CreateCheckoutSessionAsync(currency);
            if (session == null)
                return BadRequest(new { Message = "Failed to create checkout session." });

            return Ok(new { SessionUrl = session.Url });
        }

        // POST: api/Payment/Webhook
        [HttpPost("Webhook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            var result = await _paymentService.HandleStripeWebhookAsync(Request);
            if (!result)
                return BadRequest(new { Message = "Webhook handling failed." });

            return Ok(new { Message = "Webhook handled successfully." });
        }
    }
}