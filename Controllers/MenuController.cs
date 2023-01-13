using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using TiyatroProje.Models;

namespace TiyatroProje.Controllers
{
    public class MenuController : Controller
    {
        MenuManager menuManager = new MenuManager(new EfMenuRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult sil(int id)
        {
            var menu = menuManager.MenuGetirById(id);
            menu.Menusilindi = true;
            menuManager.MenuGuncelle(menu);
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult guncelle(int id)
        {
            var menu = menuManager.MenuGetirById(id);
            var menulist = menuManager.MenuListele();
            MenuParentListModel menuParentListModel = new MenuParentListModel();
            menuParentListModel.MenuListModel = menulist;
            menuParentListModel.MenuModel = menu;
            return View(menuParentListModel);
        }
        [HttpPost]
        public IActionResult guncelle(Menu menu)
        {
            menuManager.MenuGuncelle(menu);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult ekle()
        {

            var menulist = menuManager.MenuListele();
            MenuParentListModel menuParentListModel = new MenuParentListModel();
            menuParentListModel.MenuListModel = menulist;
            menuParentListModel.MenuModel = new Menu();
            return View(menuParentListModel);
        }
        [HttpPost]
        public IActionResult ekle(Menu menu)
        {
            var sonuc = menuManager.MenuGetirBySeoUrl(menu.SeoUrl);
            if (sonuc != null)
            {
                var menulist = menuManager.MenuListele();
                MenuParentListModel menuParentListModel = new MenuParentListModel();
                menuParentListModel.MenuListModel = menulist;
                menuParentListModel.MenuModel = new Menu();
                return View(menuParentListModel);
            }
            menuManager.MenuEkle(menu);
            return RedirectToAction("Index");
        }
    }
}
