using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteSushi.Models;
using PagedList;
using System.IO;
using System.Net.Mail;
using System.Text;

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
        public ActionResult NewsDetail(int id)
        {
            var result = db.News.FirstOrDefault(n => n.id_news == id);
            return View(result);
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
        public bool Reserved(Reservation item)
        {
            Reservation mod = new Reservation();
            mod.BookDate = item.BookDate;
            mod.BookTime = item.BookTime;
            mod.NumberOfPeople = item.NumberOfPeople;
            mod.NameBookedPeople = item.NameBookedPeople;
            mod.Email = item.Email;
            mod.PhoneNumber = item.PhoneNumber;
            mod.MoreRequest = item.MoreRequest;
            mod.Status = false;
            db.Reservations.Add(mod);
            db.SaveChanges();
            // send mail to people who using our service
            StringBuilder body = new StringBuilder();
            body.Append("<body>");
            body.Append("<strong>Trân trọng cảm ơn Quý khách!</strong> Thông tin đặt bàn của Quý khách dã được gửi đến bộ phận Chăm sóc Khách hàng. Trong vòng 24 giờ, chúng tôi sẽ liên lạc theo thông tin Quý khách đã đăng ký để xác nhận tình trạng đặt bàn.<br/><br/>");
            body.Append("<table style='border-collapse:collapse; width:100%'>");
            body.Append("<tr><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Ngày đặt</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Giờ đặt</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Số người</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Họ tên người đặt</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Email</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Số điện thoại</th><th style='text-align:center;padding:20px;background-color:#4CAF50;color:#fff'>Yêu cầu thêm</th></tr>");
            body.Append("<tr><td style='text-align:center;padding:20px;'>" + mod.BookDate.Value.Date + "</td><td style='text-align:center;padding:20px;'>" + mod.BookTime + "</td><td style='text-align:center;padding:20px;'>" + mod.NumberOfPeople + "</td><td style='text-align:center;padding:20px;'>" + mod.NameBookedPeople + "</td><td style='text-align:center;padding:20px;'>" + mod.Email + "</td><td style='text-align:center;padding:20px;'>" + mod.PhoneNumber + "</td><td style='text-align:center;padding:20px;'>" + mod.MoreRequest + "</td></tr>");
            body.Append("</table>");
            body.Append("</body>");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("cnttk63b@gmail.com", "SushiStar Administrator", Encoding.UTF8);
            mail.To.Add(mod.Email);
            mail.Subject = "Sushi Star - Reservation Confirm";
            mail.Body = body.ToString();
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            // add the credentails
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential("cnttk63b@gmail.com", "Lopbcntt");
            client.EnableSsl = true;
            client.Send(mail);
            return true;
        }
        public ActionResult ContactPartial()
        {
            return View();
        }
        public bool Contacted(Contact cont)
        {
            Contact mod = new Contact();
            mod.ContactName = cont.ContactName;
            mod.ContactTitle = cont.ContactTitle;
            mod.ContactEmail = cont.ContactEmail;
            mod.ContactTitleContent = cont.ContactTitleContent;
            mod.ContactStatus = false;
            db.Contacts.Add(mod);
            db.SaveChanges();
            return true;
        }
        public ActionResult SubcribePartial()
        {
            return View();
        }
    }
}