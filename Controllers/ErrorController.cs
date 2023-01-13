using Microsoft.AspNetCore.Mvc;


namespace TiyatroProje.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("/Error/Index/{code:int}")]
        public IActionResult Index(int code)
        {
            ViewBag.code = code.ToString();
            switch (code)
            {
                case 404:
                    ViewBag.msg = "Uh Oh! Page not found!";
                    break;
                case 500:
                    ViewBag.msg = "Uh Oh! Internal Error!";
                    break;
            }

            return View();
        }
    }
}
