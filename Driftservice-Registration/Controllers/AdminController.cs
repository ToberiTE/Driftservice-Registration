using Driftservice_Registration.Models;
using System.Data.Entity;
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

        public ActionResult Administration()
        {
            return View();
        }

        public async Task<JsonResult> GetContacts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var contact = await db.Contacts.ToListAsync();
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetContactById(int id)
        {
            var contact = await db.Contacts.FindAsync(id);
            return PartialView("_EditContact", contact);
        }

        public async Task<JsonResult> EditContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public async Task<ActionResult> EditContact([Bind(Include = "FirstName,LastName,Business,Email,PhoneNumber,NotificationType,Language")] Contact contact, int id)
        //{
        //    await db.Contacts.FindAsync(id);
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(contact).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //    }
        //    return Json(contact, JsonRequestBehavior.AllowGet);
        //}

        public async Task<ActionResult> DeleteContact(int id)
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

        //public async Task<JsonResult> CreateServiceType(ServiceType service)
        //{
        //    await db.SaveChangesAsync();
        //    return Json(db.ServiceTypes.Add(service), JsonRequestBehavior.AllowGet);
        //}

        public async Task<JsonResult> DeleteServiceType(int? id)
        {
            var service = await db.ServiceTypes.FindAsync(id);
            await db.SaveChangesAsync();
            return Json(db.ServiceTypes.Remove(service), JsonRequestBehavior.AllowGet);
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
