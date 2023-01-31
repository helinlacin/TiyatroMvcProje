using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class MusteriController : Controller
    {
        MusteriManager mm = new MusteriManager(new EfMusteriRepository());
        public IActionResult Index(int page = 1, string searchText = "")
        {
            int pageSize = 2;
            Context c = new Context();
            Pager pager;
            List<Musteri> data;
            var itemCounts = 0;
            if (searchText != "" && searchText != null)
            {
              data=c.Musteriler.Where(musteri=>musteri.Ad.Contains(searchText) ||musteri.Soyad.Contains(searchText) ||
              musteri.Mail.Contains(searchText) || musteri.DogumTarihi.ToString().Contains(searchText)
              ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = c.Musteriler.Where(musteri => musteri.Ad.Contains(searchText) || musteri.Soyad.Contains(searchText) ||
              musteri.Mail.Contains(searchText) || musteri.DogumTarihi.ToString().Contains(searchText)).ToList().Count;
            }
            else
            {
                data = c.Musteriler.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = c.Musteriler.ToList().Count;
            }

            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "Musteri";
            ViewBag.actionName = "Listele";
            return View(data);


            //var musteriler = mm.MusteriListele();
            //return View(musteriler);
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
        {
            Musteri musteri = mm.MusteriGetirById(id);
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
