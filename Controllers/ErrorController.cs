using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
