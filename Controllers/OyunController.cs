using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.Models;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class OyunController : Controller
    {
        OyunManager om = new OyunManager(new EfOyunRepository());
        TurManager tm= new TurManager(new EfTurRepository());
        OyunKadrosuManager okm=new OyunKadrosuManager(new EfOyunKadrosuRepository());
        public IActionResult Index(int page = 1, string searchText = "")
        {
            int pageSize = 2;
            Context c = new Context();
            Pager pager;
            List<Oyun> data;
            var itemCounts = 0;
            if (searchText != "" && searchText != null)
            {
               data=c.Oyunlar.Where(oyun=>oyun.Konu.Contains(searchText) || oyun.Afis.Contains(searchText)
               ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = c.Oyunlar.Where(oyun => oyun.Konu.Contains(searchText) || oyun.Afis.Contains(searchText)
                ).ToList().Count;
            }
            else
            {
                data = c.Oyunlar.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = c.Oyunlar.ToList().Count;
            }

            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "Oyun";
            ViewBag.actionName = "listele";
            return View(data);
            //var oyunlar = om.OyunListele();
            //return View(oyunlar);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            OyunTurVeKadrosuModel model=new OyunTurVeKadrosuModel();
            model.turModal=tm.TurListele();
            model.oyunKadrosuModal=okm.OyunKadrosuListele();
            
            return View(model);
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
                OyunTurVeKadrosuModel model = new OyunTurVeKadrosuModel();
                model.turModal = tm.TurListele();
                model.oyunKadrosuModal = okm.OyunKadrosuListele();
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(model);
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
            OyunTurVeKadrosuModel model = new OyunTurVeKadrosuModel();
            model.turModal = tm.TurListele();
            model.oyunKadrosuModal = okm.OyunKadrosuListele();
            model.oyunModal = om.OyunGetirById(id);
            return View(model);
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
                OyunTurVeKadrosuModel model = new OyunTurVeKadrosuModel();
                model.turModal = tm.TurListele();
                model.oyunKadrosuModal = okm.OyunKadrosuListele();
                model.oyunModal = oyun;
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(model);
            }

        }
    }
}
