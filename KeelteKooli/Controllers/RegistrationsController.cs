using KeelteKooli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeelteKooli.Controllers
{
    public class RegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations
                .Include("ApplicationUser")
                .Include("Training")
                .ToList();

            return View(registrations);
        }

        public ActionResult Approve(int id)
        {
            var reg = db.Registrations.Find(id);
            if (reg != null)
            {
                reg.Staatus = RegistrationStatus.Kinnitatud;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            var reg = db.Registrations.Find(id);
            if (reg != null)
            {
                reg.Staatus = RegistrationStatus.Tuhistatud;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}