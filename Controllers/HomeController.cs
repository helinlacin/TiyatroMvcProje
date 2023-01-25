using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TiyatroProje.Models;

namespace TiyatroProje.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		Context x;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
            x = new Context();
        }

		public IActionResult Index()
		{
            var menus=x.Menu.ToList();
            var mappedTree = mapListToTreview(menus);
            MenuTiyatroSliderModel msmodel = new MenuTiyatroSliderModel();
            TiyatroSliderManager sm = new TiyatroSliderManager(new EfTiyatroSliderRepository());
            msmodel.MenuModel = mappedTree;
            msmodel.TiyatroSliderModel = sm.SliderListele();
            return View(msmodel);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        public IActionResult deneme()
        {
            return View();
        }

        private List<Menu> mapListToTreview(List<Menu> menus)
        { //Menü listesi dönen bir fonksiyon oluşturdum. Parametre olarak menü listesi yollanıyor.
            var altMenuler = new List<Menu>(); // alt menüler için bir menü liste oluşturdum.
            foreach (var menu in menus)
            { //parametre olarak alınan menü listesini foreach ile döndürüyorum.
                altMenuler.Add(new Menu
                { //dönülen listeyi oluşturdugum alt listeye ekledim.
                    menuId = menu.menuId,
                    parentId = menu.parentId,
                    name = menu.name,
                    Children = menu.Children.Count > 0 ? mapListToTreview(menu.Children.ToList()) // childleri varsa tekrardan recursive ediyor. Fonksiyon tekrardan çalışıyor. Yoksada boş liste dönüyor.
                        : new List<Menu>()
                });
            }
            return altMenuler;
        }
    }
}