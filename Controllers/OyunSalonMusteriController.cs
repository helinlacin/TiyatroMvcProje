using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class OyunSalonMusteriController : Controller
    {
        OyunSalonMusteriManager blt = new OyunSalonMusteriManager(new EfOyunSalonMusteriRepository());
        public IActionResult Index(int page=1, string searchText = "")
        {
            int pageSize = 2;
            Context blt = new Context();
            Pager pager;
            List<OyunSalonMusteri> data;
            var itemCounts = 0;
            if (searchText != "" && searchText != null)
            {
                data=blt.Biletler.Where(biletler=>biletler.KoltukNo.Contains(searchText)
                ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = blt.Biletler.Where(biletler => biletler.KoltukNo.Contains(searchText)).ToList().Count();
            }
            else
            {
                data = blt.Biletler.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = blt.Biletler.ToList().Count;
            }
            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "OyunSalonMusteri";
            ViewBag.actionName = "listele";
            return View(data);
            //var OyunSalonMusteri = blt.BiletListele();
            //return View(OyunSalonMusteri);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(OyunSalonMusteri oyunSalonMusteri)
        {
            OyunSalonMusteriValidator oyunSalonMusteriValidator = new OyunSalonMusteriValidator();
            var result = oyunSalonMusteriValidator.Validate(oyunSalonMusteri);
            if (result.IsValid)
            {
                blt.BiletEkle(oyunSalonMusteri);
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
            OyunSalonMusteri oyunSalonMusteri = blt.BiletGetirById(id);
            oyunSalonMusteri.biletSilindi = true;
            blt.BiletGuncelle(oyunSalonMusteri);
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            OyunSalonMusteri oyunSalonMusteri = blt.BiletGetirById(id);
            return View(oyunSalonMusteri);
        }
        [HttpPost]
        public IActionResult Guncelle(OyunSalonMusteri oyunSalonMusteri)
        {
            OyunSalonMusteriValidator oyunSalonMusteriValidator = new OyunSalonMusteriValidator();
            var result = oyunSalonMusteriValidator.Validate(oyunSalonMusteri);
            if (result.IsValid)
            {
                blt.BiletGuncelle(oyunSalonMusteri);
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
