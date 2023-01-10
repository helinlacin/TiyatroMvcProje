using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class SalonController : Controller
    {
        SalonManager sm = new SalonManager(new EfSalonRepository());
        public IActionResult Index()
        {
            var Salon = sm.SalonListele();
            return View(Salon);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Salon salon)
        {
            SalonValidator salonValidator = new SalonValidator();
            var result = salonValidator.Validate(salon);
            if (result.IsValid)
            {
                sm.SalonEkle(salon);
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }
        public IActionResult Sil(int id)
        {
            Salon salon = sm.SalonGetirById(id);
            salon.SalonSilindi = true;
            sm.SalonGuncelle(salon);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            Salon salon=sm.SalonGetirById(id);
            return View(salon);
        }
        [HttpPost]
        public IActionResult Guncelle(Salon salon)
        {
            SalonValidator salonValidator = new SalonValidator();
            var result = salonValidator.Validate(salon);
            if (result.IsValid)
            {
                sm.SalonGuncelle(salon);
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }
    }
}
