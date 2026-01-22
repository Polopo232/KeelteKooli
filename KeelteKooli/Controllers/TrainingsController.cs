using KeelteKooli.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

public class TrainingsController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        if (!db.Trainings.Any())
        {
            var teacher = new Teacher { Nimi = "Mari Spek" };
            var course = new Course { Nimetus = "Eesti Keelt A1", Keel = "Estonian", Tase = "A1" };
            db.Teachers.Add(teacher);
            db.Courses.Add(course);
            db.SaveChanges();

            var training = new Training
            {
                CourseId = course.Id,
                Teacher = teacher,
                AlgusKuupaev = DateTime.Today,
                LoppKuupaev = DateTime.Today.AddMonths(1),
                Hind = 150,
                MaxOsalejaid = 10
            };
            db.Trainings.Add(training);
            db.SaveChanges();
        }

        var trainings = db.Trainings
            .Include(t => t.Course)
            .Include(t => t.Teacher)
            .Include(t => t.Registrations)
            .ToList();

        return View(trainings);
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
