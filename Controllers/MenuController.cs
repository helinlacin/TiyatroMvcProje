using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.Models;

namespace TiyatroProje.Controllers
{
    public class menuController : Controller
    {
        MenuManager menuManager=new MenuManager(new EfMenuRepository());  
        public IActionResult Index()
        {
            var menus = menuManager.menuListele();
            return View(menus);
        }
        public IActionResult sil(int id)
        {
            var menu = menuManager.menuGetirById(id);
            menu.MenuSilindi = true;
            menuManager.menuGuncelle(menu);
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var menu = menuManager.menuGetirById(id);
            var menulist = menuManager.menuListele();
            MenuParentListModel menuParentListModel = new MenuParentListModel();
            menuParentListModel.MenuListModel = menulist;
            menuParentListModel.MenuModel = menu;
            return View(menuParentListModel);
        }
        [HttpPost]
        public IActionResult Guncelle(Menu menu)
        {
            menuManager.menuGuncelle(menu);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult ekle()
        {

            var menulist = menuManager.menuListele();
            MenuParentListModel menuParentListModel = new MenuParentListModel();
            menuParentListModel.MenuListModel = menulist;
            menuParentListModel.MenuModel = new Menu();
            return View(menuParentListModel);
        }
        [HttpPost]
        public IActionResult Ekle(Menu menu)
        {
            var sonuc = menuManager.menuGetirBySeoUrl(menu.seoUrl);
            if (sonuc != null)
            {
                var menulist = menuManager.menuListele();
                MenuParentListModel menuParentListModel = new MenuParentListModel();
                menuParentListModel.MenuListModel = menulist;
                menuParentListModel.MenuModel = new Menu();
                return View(menuParentListModel);
            }
            menuManager.menuEkle(menu);
            return RedirectToAction("Index");
        }
    }
}
