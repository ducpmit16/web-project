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
    public class HomeController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuPartial()
        {
            return View(db.Menus.ToList());
        }
        public ActionResult NewsPartial(int? page, string key)
        {
            var value = db.News.Where(r => string.IsNullOrEmpty(key) || r.name_news.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchNewsByName(string nm, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var mod = db.News.Where(n => n.name_news.Contains(nm)).ToList();
            ViewBag.key = nm;
            return View("NewsPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NewsRandomPartial()
        {
            return View(db.News.OrderBy(x => Guid.NewGuid()).Take(4).ToList());
        }
        public ActionResult EventPartial(int? page, string key)
        {
            var value = db.Events.Where(r => string.IsNullOrEmpty(key) || r.name_event.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ConceptPartial()
        {
            return View(db.Menu_Detail.OrderBy(n => Guid.NewGuid()).Take(9).ToList());
        }
        public ActionResult ReservationPartial()
        {
            return View();
        }
        public ActionResult ContactPartial()
        {
            return View();
        }
        public ActionResult SubcribePartial()
        {
            return View();
        }
    }
}