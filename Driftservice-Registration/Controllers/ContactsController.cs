using Driftservice_Registration.Models;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Driftservice_Registration.Controllers
{
    public class ContactsController : Controller
    {
        private DriftserviceDbModel db = new DriftserviceDbModel();
        private readonly MailService mailService = new MailService();

        public ServiceType MapModelToServiceType(ServiceTypeVM svm)
        {
            return new ServiceType()
            {
                ServiceTypeID = svm.ServiceTypeID,
                Description = svm.Description,
                PublicServiceType = svm.PublicServiceType
            };
        }

        public ActionResult Registration()
        {
            ViewBag.item = db.ServiceTypes.Where(s => s.PublicServiceType == true).Select(s => new ServiceTypeVM { ServiceTypeID = s.ServiceTypeID, Description = s.Description, PublicServiceType = s.PublicServiceType }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration([Bind(Include = "ContactID,FirstName,LastName,Business,Email,PhoneNumber,SmsChecked,EmailChecked,NotificationType,ContactGuid,Language,RegDate,ServiceTypeID,Description,PublicServiceType")] Contact contact, ServiceTypeVM svm)
        {
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

            if (contact.SmsChecked && string.IsNullOrEmpty(contact.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "* Vald kontaktmetod kräver ett telefonnummer.");
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

            if (ModelState.IsValid) /* TODO: "success message box" + "creating" (before) */
            {
                MapModelToServiceType(svm);
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                var mailsent = await mailService.SendMail(contact);
                return RedirectToAction("Registration");
            }
            return View(contact); /* TODO: "error message box" */
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
