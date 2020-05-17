using Driftservice_Registration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Driftservice_Registration.Controllers
{
    public class AdminController : Controller
    {
        private DriftserviceDbModel db = new DriftserviceDbModel();

        public ActionResult Login()
        {
            return View("Login");
        }

        public async Task<ActionResult> Administration()
        {
            var listitems = new List<SelectListItem>();
            listitems = await db.ServiceTypes
                .Select(s => new SelectListItem
                {
                    Value = s.ServiceTypeID.ToString(),
                    Text = s.Description
                }).ToListAsync();
            ViewBag.FilterByService = listitems;
            return View();
        }

        public async Task<JsonResult> GetContacts(int filterByServiceId = 0)
        {
            if (filterByServiceId != 0)
            {
                var contacts = await db.Contacts
                .Include(c => c.ServiceTypes.Select(s => s.ServiceTypeID))
                .Select(x => new
                {
                    x.ContactID,
                    x.FirstName,
                    x.LastName,
                    x.Business,
                    x.Email,
                    x.PhoneNumber,
                    x.NotificationType,
                    x.Language,
                    x.RegDate,
                    ServiceTypeID = x.ServiceTypes.Select(s => s.ServiceTypeID)
                }).Where(s => s.ServiceTypeID.Contains(filterByServiceId))
                .ToListAsync();
                return Json(contacts, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var contacts = await db.Contacts
                .Include(c => c.ServiceTypes.Select(s => s.ServiceTypeID))
                .Select(x => new
                {
                    x.ContactID,
                    x.FirstName,
                    x.LastName,
                    x.Business,
                    x.Email,
                    x.PhoneNumber,
                    x.NotificationType,
                    x.Language,
                    x.RegDate,
                    ServiceTypeID = x.ServiceTypes.Select(s => s.ServiceTypeID)
                }).Where(s => s.ServiceTypeID.Any())
                .ToListAsync();
                return Json(contacts, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> NewContact(Contact contact)
        {
            ViewBag.item = await db.ServiceTypes
                .Where(s => s.PublicServiceType == true)
                .ToListAsync();
            return PartialView("_CreateContact", contact);
        }

        public async Task<JsonResult> CreateContact(Contact contact)
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

            var serviceList = new List<ServiceType>();
            foreach (var stype in contact.ServiceTypes)
            {
                var foundService = await db.ServiceTypes.FindAsync(stype.ServiceTypeID);
                serviceList.Add(foundService);
            }
            contact.ServiceTypes = serviceList;

            if (ModelState.IsValid)
            {
                contact.RegDate = DateTime.Today;
                contact.ContactGuid = Guid.NewGuid();
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                var mail = new MailService();
                await mail.SendMail(contact);
            }
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetContactById(int id)
        {
            var contact = await db.Contacts.FindAsync(id);
            var list = await db.ServiceTypes
                .Where(s => s.PublicServiceType == true)
                .ToListAsync();
            var listvm = new List<ServiceTypeVM>();
            foreach (var item in list)
            {
                listvm.Add(new ServiceTypeVM { Id = item.ServiceTypeID, Description = item.Description,
                    IsChecked = contact.ServiceTypes.Any(s => s.ServiceTypeID == item.ServiceTypeID) });
            }
            ViewBag.item = listvm;
            return PartialView("_EditContact", contact);
        }

        public async Task<JsonResult> EditContact(Contact contact)
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

            var foundContact = await db.Contacts.FindAsync(contact.ContactID);
            foundContact.ServiceTypes.Clear();
            foreach (var stype in contact.ServiceTypes)
            {
                var foundstype = await db.ServiceTypes.FindAsync(stype.ServiceTypeID);
                foundContact.ServiceTypes.Add(foundstype);
            }

            foundContact.FirstName = contact.FirstName;
            foundContact.LastName = contact.LastName;
            foundContact.Business = contact.Business;
            foundContact.Email = contact.Email;
            foundContact.PhoneNumber = contact.PhoneNumber;
            foundContact.NotificationType = contact.NotificationType;
            foundContact.Language = contact.Language;

            if (ModelState.IsValid)
            {
                db.Entry(foundContact).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteContact(int id)
        {
            var contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetServiceTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var service = await db.ServiceTypes.ToListAsync();
            return Json(service, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewServiceType(ServiceType service)
        {
            return PartialView("_CreateService", service);
        }

        public async Task<JsonResult> CreateServiceType(ServiceType service)
        {
            db.ServiceTypes.Add(service);
            await db.SaveChangesAsync();
            return Json(service, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetServiceById(int id)
        {
            var service = await db.ServiceTypes.FindAsync(id);
            return PartialView("_EditService", service);
        }

        public async Task<JsonResult> EditServiceType(ServiceType service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Json(service, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteServiceType(int id)
        {
            var service = await db.ServiceTypes.FindAsync(id);
            db.ServiceTypes.Remove(service);
            await db.SaveChangesAsync();
            return Json(true, JsonRequestBehavior.AllowGet);
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
