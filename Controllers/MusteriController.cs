using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class MusteriController : Controller
    {
        MusteriManager mm = new MusteriManager(new EfMusteriRepository());
        public IActionResult Index()
        {
            var musteriler = mm.MusteriListele();
            return View(musteriler);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Musteri musteri)
        {
            MusteriValidator MusteriValidator = new MusteriValidator();
            var result = MusteriValidator.Validate(musteri);
            if (result.IsValid)
            {
                mm.MusteriEkle(musteri);
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
        {  Musteri musteri=mm.MusteriGetirById(id);
            musteri.MusteriSilindi = true;
            mm.MusteriGüncelle(musteri);
            return RedirectToAction("Index");  
        }
        public IActionResult Guncelle(int id)
        {
            Musteri musteri = mm.MusteriGetirById(id);
            return View(musteri);
        }
        [HttpPost]
        public IActionResult Guncelle(Musteri musteri)
        {
            MusteriValidator musteriValidator = new MusteriValidator();
            var result = musteriValidator.Validate(musteri);
            if (result.IsValid)
            {
                mm.MusteriGüncelle(musteri);
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
