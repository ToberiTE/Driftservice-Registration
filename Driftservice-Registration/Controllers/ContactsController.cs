using Driftservice_Registration.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Driftservice_Registration.Controllers
{
    public class ContactsController : Controller
    {
        private DriftserviceDbModel db = new DriftserviceDbModel();

        public async Task<ActionResult> Registration()
        {
            ViewBag.item = await db.ServiceTypes
                .Where(s => s.PublicServiceType == true)
                .ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(Contact contact, string captcha)
        {
            ViewBag.item = await db.ServiceTypes
                .Where(s => s.PublicServiceType == true)
                .ToListAsync();

            if (contact.EmailChecked && !contact.SmsChecked)
            {
                contact.NotificationType = 1;
            }
            else if (!contact.EmailChecked && contact.SmsChecked)
            {
                contact.NotificationType = 2;
            }
            else if (contact.SmsChecked && contact.EmailChecked)
            {
                contact.NotificationType = 3;
            }

            if (string.IsNullOrEmpty(captcha))
            {
                ModelState.AddModelError("reCAPTCHA", "* reCAPTCHA error.");
                return Json(contact, JsonRequestBehavior.AllowGet);
            }
            using (var wc = new WebClient())
            {
                var secret = WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
                var response = string.Format(
                    "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                   secret,
                   captcha);
                var result = wc.DownloadString(response);
                if (result.ToLower().Contains("false"))
                {
                    ModelState.AddModelError("reCAPTCHA", "* reCAPTCHA error.");
                    return Json(contact, JsonRequestBehavior.AllowGet);
                }
            }

            var serviceList = new List<ServiceType>();
            foreach (var stype in contact.ServiceTypes)
            {
                var foundService = await db.ServiceTypes.FindAsync(stype.ServiceTypeID);
                serviceList.Add(foundService);
            }
            contact.ServiceTypes = serviceList;

            if (ModelState.IsValid)
            {
                contact.ContactGuid = Guid.NewGuid();
                contact.RegDate = DateTime.Today;
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                var mail = new MailService();
                await mail.SendMail(contact);
                return Json("Create", JsonRequestBehavior.AllowGet);
            }
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
