using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class OyunController : Controller
    {
        OyunManager om = new OyunManager(new EfOyunRepository());
        public IActionResult Index()
        {
            var oyunlar = om.OyunListele();
            return View(oyunlar);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Oyun oyun)
        {
            OyunValidator oyunvalidator = new OyunValidator();
            var result =oyunvalidator.Validate(oyun);   
            if (result.IsValid)
            {
                om.OyunEkle(oyun);
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
            Oyun oyun = om.OyunGetirById(id);
            oyun.OyunSilindi = true;
            om.OyunGüncelle(oyun);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            Oyun oyun = om.OyunGetirById(id);
            return View(oyun);
        }
        [HttpPost]
        public IActionResult Guncelle(Oyun oyun)
        {
            OyunValidator oyunvalidator = new OyunValidator();
            var result = oyunvalidator.Validate(oyun);
            if (result.IsValid)
            {
                om.OyunGüncelle(oyun);
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
