using Microsoft.AspNetCore.Mvc;

namespace PaymentAndNotifications.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Features/Index.cshtml");
        }

        public IActionResult About()
        {
            return View("~/Views/Features/About.cshtml");
        }

        public IActionResult Contact()
        {
            return View("~/Views/Features/Contact.cshtml");
        }

        public IActionResult Error()
        {
            return View("~/Views/Features/Error.cshtml");
        }

        public IActionResult Payments()
        {
            return View("~/Views/Features/Payments.cshtml");
        }

        public IActionResult Emails()
        {
            return View("~/Views/Features/Emails.cshtml");
        }

        public IActionResult Notifications()
        {
            return View("~/Views/Features/Notifications.cshtml");
        }
    }
}