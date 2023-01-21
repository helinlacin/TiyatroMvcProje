using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace TiyatroProje.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly IToastNotification _toastNotification;

        public AdminController(ILogger<AdminController> logger, IToastNotification toastNotification)
        {
            _logger = logger;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();

        }
        public IActionResult profile()
        {
            return View();
        }
        

    }
}
