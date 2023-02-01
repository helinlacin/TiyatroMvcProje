using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.PagedList;

namespace TiyatroProje.Controllers
{
    public class TiyatroSliderController : Controller
    {   TiyatroSliderManager tsm= new TiyatroSliderManager(new EfTiyatroSliderRepository());

        public IActionResult Index(int page = 1, string searchText = "")
        {
            int pageSize = 2;
            Context ts = new Context();
            Pager pager;
            List<TiyatroSlider> data;
            var itemCounts = 0;
            if(searchText != "" && searchText != null)
            {
                data=ts.TiyatroSlider.Where(tiyatroSlider=>tiyatroSlider.SliderName.Contains(searchText) 
                ).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts=ts.TiyatroSlider.Where(tiyatroSlider => tiyatroSlider.SliderName.Contains(searchText)).ToList().Count;
            }
            else
            {
                data = ts.TiyatroSlider.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                itemCounts = ts.TiyatroSlider.ToList().Count;
            }
            pager = new Pager(itemCounts, pageSize, page);
            ViewBag.pager = pager;
            ViewBag.searchText = searchText;
            ViewBag.contrName = "tiyatroslider";
            ViewBag.actionName = "listele";
            return View(data);
            //return View(tsm.SliderListele());
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
