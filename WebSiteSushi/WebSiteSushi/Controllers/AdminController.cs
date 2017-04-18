using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteSushi.Models;

namespace WebSiteSushi.Controllers
{
    public class AdminController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var obj = db.Users.FirstOrDefault(n => n.username == objUser.username && n.password == objUser.password);
                    if (obj != null)
                    {
                        Session["id_user"] = obj.id_user.ToString();
                        Session["username"] = obj.username.ToString();
                        Session["password"] = obj.password.ToString();
                        Session["fullname"] = obj.fullname.ToString();
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.status = "Username Or Password is not correct !!!";
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult Dashboard()
        {
            ViewBag.reserv = db.Reservations.Count(n => n.Status == false && (n.BookDate >= DateTime.Now));
            ViewBag.mail = db.Contacts.Count(n => n.ContactStatus == false);
            if (Session["id_user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}