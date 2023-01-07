using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class OyunSalonController : Controller
    {
        OyunSalonManager osm = new OyunSalonManager(new EfOyunSalonRepository());
        public IActionResult Index()
        {
            var oyunsalon = osm.SeansListele();
            return View(oyunsalon);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(OyunSalon oyunSalon)
        {
            OyunSalonValidator oyunSalonValidator = new OyunSalonValidator();
            var result = oyunSalonValidator.Validate(oyunSalon);
            if (result.IsValid)
            {
                osm.SeansEkle(oyunSalon);
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
            OyunSalon oyunSalon = osm.SeansGetirById(id);
            oyunSalon.SeansSilindi = true;
            osm.SeansGuncelle(oyunSalon);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            OyunSalon oyunSalon = osm.SeansGetirById(id);
            return View(oyunSalon);
        }
        [HttpPost]
        public IActionResult Guncelle(OyunSalon oyunSalon)
        {
            OyunSalonValidator oyunSalonValidator = new OyunSalonValidator();
            var result = oyunSalonValidator.Validate(oyunSalon);
            if (result.IsValid)
            {
                osm.SeansGuncelle(oyunSalon);
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
