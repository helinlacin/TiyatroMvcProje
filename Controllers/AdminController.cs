using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;
using NToastNotify;

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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult login()
        {
            return View();

        }
        [HttpGet]
        public IActionResult profile()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Giris(Admin admin)
        {

            Context c = new Context();
            var result = c.Adminler.Where(x => x.AdminEmail == admin.AdminEmail && x.AdminPassword == admin.AdminPassword).SingleOrDefault();
            if (result != null)
            {

                var claims = new List<Claim> { new Claim(ClaimTypes.Email, result.AdminEmail) };

                var userIdentify = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentify);
                //  await HttpContext.SignInAsync(principal);
                await HttpContext
                    .SignInAsync(
                    principal,
                    new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(1) });
                return RedirectToAction("profile", "Admin");
            }
            _toastNotification.AddErrorToastMessage("Kullanıcı adı veya password hatalı");
            TempData["init"] = 1;
            return RedirectToAction("login");
        }
        public async Task<IActionResult> Cikis()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");

        }

    }
}
