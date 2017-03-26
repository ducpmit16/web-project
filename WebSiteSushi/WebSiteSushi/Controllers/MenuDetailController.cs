using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteSushi.Models;
using PagedList;
using System.IO;

namespace WebSiteSushi.Controllers
{
    public class MenuDetailController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: MenuDetail

        //public ActionResult MenuDetailPartial()
        //{
        //    return View(db.Menu_Detail.OrderBy(n => n.name_food_detail).ToList());
        //}
        public ActionResult MenuDetailPartial(int? page, string key)
        {
            var value = db.Menu_Detail.Where(r => string.IsNullOrEmpty(key) || r.name_food_detail.ToLower().Contains(key.ToLower())).OrderBy(n => n.name_food_detail).ToList();
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchMenuDetailByName(string nm, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var mod = db.Menu_Detail.Where(n => n.name_food_detail.Contains(nm)).ToList();
            ViewBag.key = nm;
            return View("MenuDetailPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            ViewBag.listMenu = db.Menus.Select(r => new SelectListItem() { Value = r.id_menu.ToString(), Text = r.name_food }).ToList();
            return View();
        }
        public bool saveMenuDetail(Menu_Detail menuDetail)
        {
            Menu_Detail menuDetailItem = new Menu_Detail();
            menuDetailItem.name_food_detail = menuDetail.name_food_detail;
            menuDetailItem.price_food_detail = menuDetail.price_food_detail;
            menuDetailItem.id_menu = menuDetail.id_menu;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file != null && file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/menu-detail"), file.FileName);
                    file.SaveAs(path);
                }
            }
            menuDetailItem.image_food_detail = Convert.ToString(file.FileName);
            db.Menu_Detail.Add(menuDetailItem);
            db.SaveChanges();
            return true;
        }
        public ActionResult Update(int id)
        {
            ViewBag.listMenu = db.Menus.Select(r => new SelectListItem() { Value = r.id_menu.ToString(), Text = r.name_food }).ToList();
            var result = db.Menu_Detail.FirstOrDefault(n => n.id_menu_detail == id);
            return View(result);
        }
        public bool updateMenuDetail(Menu_Detail item)
        {
            var mod = db.Menu_Detail.FirstOrDefault(n => n.id_menu_detail == item.id_menu_detail);
            mod.id_menu_detail = item.id_menu_detail;
            mod.name_food_detail = item.name_food_detail;
            mod.price_food_detail = item.price_food_detail;
            mod.id_menu = item.id_menu;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file == null && file.ContentLength == 0)
            {
                mod.image_food_detail = mod.image_food_detail;
            }
            else
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/menu-detail"), file.FileName);
                    file.SaveAs(path);
                    mod.image_food_detail = Convert.ToString(file.FileName);
                }
            }
            db.SaveChanges();
            return true;
        }
        public ActionResult Delete(int id)
        {
            ViewBag.id_menu_detail = id;
            return View();
        }
        public bool deleteMenuDetail(int id_menu_detail)
        {
            Menu_Detail mod = db.Menu_Detail.FirstOrDefault(n => n.id_menu_detail == id_menu_detail);
            db.Menu_Detail.Remove(mod);
            db.SaveChanges();
            return true;
        }
    }
}