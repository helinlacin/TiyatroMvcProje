using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace TiyatroProje.Controllers
{
    public class TiyatroSliderController : Controller
    {   TiyatroSliderManager tsm= new TiyatroSliderManager(new EfTiyatroSliderRepository());

        public IActionResult Index()
        {
            return View(tsm.SliderListele());
        }
        [HttpGet]
        public IActionResult Ekle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Ekle(TiyatroSlider tiyatroslider)
        {
            tsm.SliderEkle(tiyatroslider);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            return View(tsm.SliderGetirById(id));
        }
        [HttpPost]
        public IActionResult Guncelle(TiyatroSlider tiyatroslider)
        {
            tsm.SliderEkle(tiyatroslider);
            return RedirectToAction("Index");
        }

    }
}
