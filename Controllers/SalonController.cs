using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class SalonController : Controller
    {
        SalonManager sm = new SalonManager(new EfSalonRepository());
        public IActionResult Index(int page = 1, string searchText = "")
        {
            int pageSize = 2;
            Context sln = new Context();
            Pager pager;
            List<Salon> data;
            var itemCounts = 0;
            if (searchText != "" && searchText != null)
            {
                data=sln.Salonlar.Where(salon=>salon.SalonNo.ToString().Contains(searchText) || salon.Konum.Contains(searchText)
                ).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                itemCounts=sln.Salonlar.Where(salon => salon.SalonNo.ToString().Contains(searchText) || salon.Konum.Contains(searchText)
                ).ToList().Count;
            }
            else
            {
                data = sln.Salonlar.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = sln.Salonlar.ToList().Count;
            }

            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "Salon";
            ViewBag.actionName = "listele";
            return View(data);
            //var Salon = sm.SalonListele();
            //return View(Salon);
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
            Salon salon = sm.SalonGetirById(id);
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
