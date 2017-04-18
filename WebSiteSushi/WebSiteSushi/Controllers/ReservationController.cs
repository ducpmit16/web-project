using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteSushi.Models;
using PagedList;

namespace WebSiteSushi.Controllers
{
    public class ReservationController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: Reservation
        public ActionResult ReservationPartial(int? page, string key)
        {
            var value = db.Reservations.Where(r => string.IsNullOrEmpty(key) || r.NameBookedPeople.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchPeopleByName(string nm, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var mod = db.Reservations.Where(n => n.NameBookedPeople.Contains(nm)).ToList();
            ViewBag.key = nm;
            return View("ReservationPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchPeopleByStatus(bool stt, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<Reservation> mod = new List<Reservation>();
            if(stt == false)
            {
                mod = db.Reservations.Where(n => n.Status == stt && n.BookDate >= DateTime.Now).ToList();
            }
            else if(stt == true)
            {
                mod = db.Reservations.Where(n => n.Status == stt).ToList();
            }
            return View("ReservationPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchPeopleByBoth(string nm, bool stt, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<Reservation> mod = new List<Reservation>();
            if (stt == false)
            {
                mod = db.Reservations.Where(n => n.Status == stt && n.NameBookedPeople.Contains(nm) && n.BookDate >= DateTime.Now).ToList();
            }
            else if (stt == true)
            {
                mod = db.Reservations.Where(n => n.Status == stt && n.NameBookedPeople.Contains(nm)).ToList();
            }
            ViewBag.key = nm;
            return View("ReservationPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Update(int id)
        {
            var result = db.Reservations.FirstOrDefault(n => n.BookID == id);
            return View(result);
        }
        public bool updateStatus(Reservation item)
        {
            var mod = db.Reservations.FirstOrDefault(n => n.BookID == item.BookID);
            item.Status = Convert.ToBoolean(1);
            mod.Status = item.Status;
            db.SaveChanges();
            return true;
        }
        public ActionResult Delete(int id)
        {
            ViewBag.id_book = id;
            return View();
        }
        public bool deleteStatus(int id_book)
        {
            Reservation mod = db.Reservations.FirstOrDefault(n => n.BookID == id_book);
            db.Reservations.Remove(mod);
            db.SaveChanges();
            return true;
        }
    }
}