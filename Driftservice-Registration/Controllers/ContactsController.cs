using Driftservice_Registration.Models;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Driftservice_Registration.Controllers
{
    public class ContactsController : Controller
    {
        private DriftserviceDbModel db = new DriftserviceDbModel();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactID,FirstName,LastName,Business,Email,PhoneNumber,SmsChecked,EmailChecked,NotificationType,ContactGuid,Language,RegDate")] Contact contact)
        {
            if (contact.SmsChecked && !contact.EmailChecked)
            {
                contact.NotificationType = 1;
            }
            else if (!contact.SmsChecked && contact.EmailChecked)
            {
                contact.NotificationType = 2;
            }
            else if (contact.SmsChecked && contact.EmailChecked)
            {
                contact.NotificationType = 3;
            }

            if (!contact.EmailChecked && !contact.SmsChecked)
            {
                ModelState.AddModelError("NotificationType", "* Välj minst en kontaktmetod.");
                return View(contact);
            }
            if (contact.EmailChecked && contact.SmsChecked && string.IsNullOrEmpty(contact.Email) && string.IsNullOrEmpty(contact.PhoneNumber))
            {
                ModelState.AddModelError("Email", "* Vald kontaktmetod kräver en email.");
                ModelState.AddModelError("PhoneNumber", "* Vald kontaktmetod kräver ett telefonnummer.");
                return View(contact);
            }
            if (contact.SmsChecked && string.IsNullOrEmpty(contact.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "* Vald kontaktmetod kräver ett telefonnummer.");
                return View(contact);
            }
            if (contact.EmailChecked && string.IsNullOrEmpty(contact.Email))
            {
                ModelState.AddModelError("Email", "* Vald kontaktmetod kräver en email.");
                return View(contact);
            }

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (string.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("reCAPTCHA", "* reCAPTCHA error.");
                return View(contact);
            }
            else
            {
                RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (recaptchaResult != RecaptchaVerificationResult.Success)
                {
                    ModelState.AddModelError("reCAPTCHA", "* reCAPTCHA error.");
                    return View(contact);
                }
            }

            if (ModelState.IsValid)
            {
                var body = "<h4>Hej, {0}!</h4></br><p>Det här är ett testmail,</p></br><p>Vi hör av oss om det händer något.</p>";
                var message = new MailMessage();
                message.From = new MailAddress("testarn123123@gmail.com");
                message.To.Add(new MailAddress(contact.Email));
                message.Subject = "Driftservice";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = string.Format(body, contact.FirstName);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("testarn123123@gmail.com", "test#123");
                    await smtp.SendMailAsync(message);
                }
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("create");
            }
            return View(contact);
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
