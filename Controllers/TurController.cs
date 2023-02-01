using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class TurController : Controller
    {
        TurManager tm = new TurManager(new EfTurRepository());
        public IActionResult Index(int page = 1, string searchText = "")
        {
            int pageSize = 2;
            Context t = new Context();
            Pager pager;
            List<Tur> data;
            var itemCounts = 0;
            if(searchText != "" && searchText != null)
            {
                data=t.Türler.Where(tur=>tur.TurAd.Contains(searchText)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts= t.Türler.Where(tur=>tur.TurAd.Contains(searchText)).ToList().Count;    
            }
            else
            {
                data = t.Türler.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = t.Türler.ToList().Count;
            }

            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "Tur";
            ViewBag.actionName = "listele";
            return View(data);
            //var Tur = tm.TurListele();
            //return View(Tur);
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
