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
    public class NewsController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: News
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
        public ActionResult Detail(int? news_Id)
        {
            var value = db.News.FirstOrDefault(n => n.id_news == news_Id);
            return View(value);
        }
        public ActionResult Create()
        {
            News newsItem = new News();
            return View(newsItem);
        }
        public bool saveNews(News news)
        {
            News newsItem = new News();
            newsItem.name_news = news.name_news;
            newsItem.poster_news = news.poster_news;
            newsItem.description_news = news.description_news;
            newsItem.date_post_news = news.date_post_news;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file != null && file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/news"), file.FileName);
                    file.SaveAs(path);
                }
            }
            newsItem.image_news = Convert.ToString(file.FileName);
            db.News.Add(newsItem);
            db.SaveChanges();
            return true;
        }
        public ActionResult Update(int id)
        {
            var result = db.News.FirstOrDefault(n => n.id_news == id);
            return View(result);
        }
        public bool updateNews(News item)
        {
            var mod = db.News.FirstOrDefault(n => n.id_news == item.id_news);
            mod.name_news = item.name_news;
            mod.poster_news = item.poster_news;
            mod.description_news = item.description_news;
            mod.date_post_news = item.date_post_news;
            var path = "";
            HttpPostedFileBase file = Request.Files["photo"];
            if (file == null && file.ContentLength == 0)
            {
                mod.image_news = mod.image_news;
            }
            else
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".gif")
                {
                    path = Path.Combine(Server.MapPath("~/Image/news"), file.FileName);
                    file.SaveAs(path);
                    mod.image_news = Convert.ToString(file.FileName);
                }
            }
            db.SaveChanges();
            return true;
        }
        public ActionResult Delete(int id)
        {
            ViewBag.id_news = id;
            return View();
        }
        public bool deleteNews(int id_news)
        {
            News mod = db.News.FirstOrDefault(n => n.id_news == id_news);
            db.News.Remove(mod);
            db.SaveChanges();
            return true;
        }
    }
}