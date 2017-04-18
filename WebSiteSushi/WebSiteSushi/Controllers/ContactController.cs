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
    public class ContactController : Controller
    {
        QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: Contact
        public ActionResult ContactPartial(int? page, string key)
        {
            var value = db.Contacts.Where(r => string.IsNullOrEmpty(key) || r.ContactTitle.ToLower().Contains(key.ToLower())).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.key = key;
            return View(value.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Feedback(int id)
        {
            var result = db.Contacts.FirstOrDefault(n => n.ContactId == id);
            return View(result);
        }
        public bool replyFeedback(Contact cont, string contact_detail)
        {
            var mod = db.Contacts.FirstOrDefault(n => n.ContactId == cont.ContactId);
            mod.ContactEmail = cont.ContactEmail;
            mod.ContactTitle = cont.ContactTitle;
            cont.ContactStatus = Convert.ToBoolean(1);
            mod.ContactStatus = cont.ContactStatus;
            db.SaveChanges();
            // send mail to people who using our service
            StringBuilder body = new StringBuilder();
            body.Append("<body>");
            body.Append(contact_detail);
            body.Append("</body>");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("cnttk63b@gmail.com", "SushiStar Administrator", Encoding.UTF8);
            mail.To.Add(mod.ContactEmail);
            mail.Subject = "Reply - " + mod.ContactTitle;
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
    }
}