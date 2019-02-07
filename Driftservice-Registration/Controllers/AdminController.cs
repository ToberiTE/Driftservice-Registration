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

        public async Task<JsonResult> GetServiceTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var service = await db.ServiceTypes.ToListAsync();
            return Json(service, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditContact(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteContact(int? id)
        {
            var contact = await db.Contacts.FindAsync(id);
            await db.SaveChangesAsync();
            return Json(db.Contacts.Remove(contact), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> CreateServiceType(ServiceType service)
        {
            await db.SaveChangesAsync();
            return Json(db.ServiceTypes.Add(service), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteServiceType(int? id)
        {
            var service = await db.ServiceTypes.FindAsync(id);
            await db.SaveChangesAsync();
            return Json(db.ServiceTypes.Remove(service), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetContactById(int id)
        {
            var contact = await db.Contacts.FindAsync(id);
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        //public async Task<ActionResult> Administration()
        //{
        //    var vm = new ListVM();
        //    vm.contact = await db.Contacts.ToListAsync();
        //    vm.serviceType = await db.ServiceTypes.ToListAsync();
        //    var vmList = new List<ListVM>();
        //    vmList.Add(vm);
        //    return View(vmList);
        //}

        //public async Task<ActionResult> Edit(int? id)
        //{
        //    Contact contact = await db.Contacts.FindAsync(id);
        //    return PartialView("_EditContact");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,Business,Email,PhoneNumber,NotificationType,Language")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(contact).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //    }
        //    return PartialView("_EditContact");
        //}

        //public async Task<ActionResult> Delete(int? id)
        //{
        //    Contact contact = await db.Contacts.FindAsync(id);
        //    return View(contact);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Contact contact = await db.Contacts.FindAsync(id);
        //    db.Contacts.Remove(contact);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Administration");
        //}

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
