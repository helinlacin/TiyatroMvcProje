using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class TurController : Controller
    {
        TurManager tm = new TurManager(new EfTurRepository());
        public IActionResult Index()
        {
            var Tur = tm.TurListele();
            return View(Tur);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Tur tur)
        {
            TurValidator turValidator = new TurValidator();
            var result = turValidator.Validate(tur);
            if (result.IsValid)
            {
                tm.TurEkle(tur);
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
            Tur tur =tm.TurGetirById(id);
            tur.turSilindi = true;
            tm.TurGuncelle(tur);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
           Tur tur=tm.TurGetirById(id);
            return View(tur);
        }
        [HttpPost]
        public IActionResult Guncelle(Tur tur)
        {
            TurValidator turValidator = new TurValidator();
            var result = turValidator.Validate(tur);
            if (result.IsValid)
            {
                tm.TurGuncelle(tur);
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
