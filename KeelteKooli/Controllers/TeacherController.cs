using System.Linq;
using System.Web.Mvc;
using KeelteKooli.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;


public class TeacherController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Dashboard()
    {
        var userId = User.Identity.GetUserId();

        var teacher = db.Teachers
            .Include("Trainings")
            .Include("Trainings.Course")
            .Include("Trainings.Registrations")
            .Include("Trainings.Registrations.ApplicationUser")
            .FirstOrDefault(t => t.ApplicationUserId == userId);

        return View(teacher);
    }

    public ActionResult Create()
    {
        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi");
        return View();
    }

    public ActionResult Create(Training training)
    {
        if (ModelState.IsValid)
        {
            training.Course = new Course { Nimetus = training.CourseName };
            db.Trainings.Add(training);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi", training.TeacherId);
        return View(training);
    }
}
