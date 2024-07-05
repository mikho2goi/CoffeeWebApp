using Microsoft.AspNetCore.Mvc;

namespace CoffeeWebApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
