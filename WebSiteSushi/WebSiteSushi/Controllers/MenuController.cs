using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteSushi.Models;
using PagedList;

namespace WebSiteSushi.Controllers
{
    public class MenuController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();   
        // GET: Menu

        //public ActionResult MenuPartial()
        //{
        //    return View(db.Menus.ToList());
        //}
        public ActionResult MenuPartial(int? page, string key)
        {
            var value = db.Menus.Where(r => string.IsNullOrEmpty(key) || r.name_food.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            Menu menuItem = new Menu();
            return View(menuItem);
        }
        public bool saveMenu(Menu menu)
        {
            Menu menuItem = new Menu();
            menuItem.name_food = menu.name_food;
            menuItem.name_id = menu.name_id;
            db.Menus.Add(menuItem);
            db.SaveChanges();
            return true;
        }

        public ActionResult Edit(int id)
        {
            var result = db.Menus.FirstOrDefault(n => n.id_menu == id);
            return View(result);
        }
        public bool editMenu(Menu menu)
        {
            var mod = db.Menus.FirstOrDefault(n => n.id_menu == menu.id_menu);
            mod.id_menu = menu.id_menu;
            mod.name_food = menu.name_food;
            mod.name_id = menu.name_id;
            db.SaveChanges();
            return true;
        }

        public ActionResult Delete(int id)
        {
            ViewBag.id_menu = id;
            return View();
        }
        public bool deleteMenu(int id_menu)
        {
            int count = db.Menu_Detail.Count(n => n.id_menu == id_menu);
            if (count > 0)
            {
                return false;
            }
            else
            {
                Menu mod = db.Menus.Where(n => n.id_menu == id_menu).Single<Menu>();
                db.Menus.Remove(mod);
                db.SaveChanges();
                return true;
            }
        }
        public ActionResult Detail(int? menu_Id)
        {
            var value = db.Menu_Detail.Where(n => n.id_menu == menu_Id).ToList();
            return View(value);
        }
        public ActionResult searchMenuByName(string nm, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var mod = db.Menus.Where(n => n.name_food.Contains(nm)).ToList();
            ViewBag.key = nm;
            return View("MenuPartial", mod.ToPagedList(pageNumber, pageSize));
        }
    }
}