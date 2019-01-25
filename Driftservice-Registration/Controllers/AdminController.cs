using Driftservice_Registration.Models;
using System.Data.Entity;
using System.Net;
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
            return View(await db.Contacts.ToListAsync());
        }



        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactID,FirstName,LastName,Business,Email,PhoneNumber,NotificationType,ContactGuid,Language,RegDate")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Administration");
            }
            return View(contact);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Administration");
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
