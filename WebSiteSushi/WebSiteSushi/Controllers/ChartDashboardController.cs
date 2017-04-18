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
            return View(db.Menus.ToList());
        }
    }
}