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
    public class EventController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: Event
        public ActionResult EventPartial(int? page, string key)
        {
            var value = db.Events.Where(r => string.IsNullOrEmpty(key) || r.name_event.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult searchEventByName(string nm, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var mod = db.Events.Where(n => n.name_event.Contains(nm)).ToList();
            ViewBag.key = nm;
            return View("EventPartial", mod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            Event eventItem = new Event();
            return View(eventItem);
        }
        public bool saveEvent(Event evnt)
        {
            Event eventItem = new Event();
            eventItem.name_event = evnt.name_event;
            eventItem.date_start = evnt.date_start;
            eventItem.date_end = evnt.date_end;
            eventItem.time_event = evnt.time_event;
            eventItem.location = evnt.location;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file != null && file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/event"), file.FileName);
                    file.SaveAs(path);
                }
            }
            eventItem.image_event = Convert.ToString(file.FileName);
            db.Events.Add(eventItem);
            db.SaveChanges();
            return true;
        }
        public ActionResult Update(int id)
        {
            var result = db.Events.FirstOrDefault(n => n.id_event == id);
            return View(result);
        }
        public bool updateEvent(Event evnt)
        {
            var mod = db.Events.FirstOrDefault(n => n.id_event == evnt.id_event);
            mod.name_event = evnt.name_event;
            mod.date_start = evnt.date_start;
            mod.date_end = evnt.date_end;
            mod.time_event = evnt.time_event;
            mod.location = evnt.location;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file == null && file.ContentLength == 0)
            {
                mod.image_event = mod.image_event;
            }
            else
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/event"), file.FileName);
                    file.SaveAs(path);
                    mod.image_event = Convert.ToString(file.FileName);
                }
            }
            db.SaveChanges();
            return true;
        }
        public ActionResult Delete(int id)
        {
            ViewBag.id_event = id;
            return View();
        }
        public bool deleteEvent(int id_event)
        {
            Event mod = db.Events.FirstOrDefault(n => n.id_event == id_event);
            db.Events.Remove(mod);
            db.SaveChanges();
            return true;
        }
    }
}