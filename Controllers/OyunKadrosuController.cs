using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class OyunKadrosuController : Controller
    {
        OyunKadrosuManager okm = new OyunKadrosuManager(new EfOyunKadrosuRepository());
        public IActionResult Index()
        {
            var oyunkadrosu = okm.OyunKadrosuListele();
            return View(oyunkadrosu);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(OyunKadrosu oyunKadrosu)
        {
            OyunKadrosuValidator oyunKadrosuvalidator = new OyunKadrosuValidator();
            var result = oyunKadrosuvalidator.Validate(oyunKadrosu);
            if (result.IsValid)
            {
                okm.OyunKadrosuEkle(oyunKadrosu);
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
            OyunKadrosu oyunKadrosu = okm.OyunKadrosuGetirById(id);
            oyunKadrosu.kadrokisisilindi = true;
            okm.OyunKadrosuGuncelle(oyunKadrosu);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            OyunKadrosu oyunKadrosu = okm.OyunKadrosuGetirById(id);
            return View(oyunKadrosu);
        }
        [HttpPost]
        public IActionResult Guncelle(OyunKadrosu oyunKadrosu)
        {
            OyunKadrosuValidator oyunKadrosuvalidator = new OyunKadrosuValidator();
            var result = oyunKadrosuvalidator.Validate(oyunKadrosu);
            if (result.IsValid)
            {
                okm.OyunKadrosuGuncelle(oyunKadrosu);
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
