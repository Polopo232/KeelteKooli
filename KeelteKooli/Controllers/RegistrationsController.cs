using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KeelteKooli.Models;

namespace KeelteKooli.Controllers
{
    public class RegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations
                .Include(r => r.Training)
                .Include(r => r.User);

            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Registration registration = db.Registrations
                .Include(r => r.Training)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);

            if (registration == null)
                return HttpNotFound();

            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.TrainingId = new SelectList(db.Trainings, "Id", "Name");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Registrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingId = new SelectList(db.Trainings, "Id", "Name", registration.TrainingId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", registration.ApplicationUserId);

            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Registration registration = db.Registrations.Find(id);
            if (registration == null)
                return HttpNotFound();

            ViewBag.TrainingId = new SelectList(db.Trainings, "Id", "Name", registration.TrainingId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", registration.ApplicationUserId);

            return View(registration);
        }

        // POST: Registrations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingId = new SelectList(db.Trainings, "Id", "Name", registration.TrainingId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", registration.ApplicationUserId);

            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Registration registration = db.Registrations
                .Include(r => r.Training)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);

            if (registration == null)
                return HttpNotFound();

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
