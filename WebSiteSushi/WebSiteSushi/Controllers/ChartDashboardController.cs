using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using WebSiteSushi.Models;

namespace WebSiteSushi.Controllers
{
    public class ChartDashboardController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: ChartDashboard
        public ActionResult ChartArrayBasic()
        {
            ViewBag.menuAll = db.Menus.Count();
            ViewBag.menusAll = db.Menu_Detail.Count();
            ViewBag.reservAll = db.Reservations.Count();
            ViewBag.mailAll = db.Contacts.Count();
            return View();
        }
    }
}