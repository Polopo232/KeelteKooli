using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using KeelteKooli.Models;
using Microsoft.AspNet.Identity;

public class TrainingsController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        var trainings = db.Trainings
            .Include(t => t.Course)
            .Include(t => t.Teacher)
            .Include(t => t.Registrations)
            .AsQueryable();

        if (User.IsInRole("Student"))
        {
            string userId = User.Identity.GetUserId();

            trainings = trainings
                .Where(t => !t.Registrations
                    .Any(r => r.ApplicationUserId == userId &&
                              r.Staatus != RegistrationStatus.Tuhistatud));
        }

        return View(trainings.ToList());
    }



    [Authorize(Roles = "Student")]
    public ActionResult Register(int id)
    {
        string userId = User.Identity.GetUserId();

        bool exists = db.Registrations.Any(r =>
            r.TrainingId == id &&
            r.ApplicationUserId == userId &&
            r.Staatus != RegistrationStatus.Tuhistatud);


        if (exists)
        {
            TempData["Message"] = "Olete juba registreerunud sellele kursusele!";
            return RedirectToAction("Index");
        }

        db.Registrations.Add(new Registration
        {
            TrainingId = id,
            ApplicationUserId = userId,
            Staatus = RegistrationStatus.Ootel
        });

        db.SaveChanges();

        TempData["Message"] = "Olete edukalt registreerunud kursusele!";
        return RedirectToAction("MyCourses");
    }

    [Authorize(Roles = "Student")]
    public ActionResult MyCourses()
    {
        string userId = User.Identity.GetUserId();

        var myTrainings = db.Registrations
            .Where(r => r.ApplicationUserId == userId && r.Staatus == RegistrationStatus.Kinnitatud)
            .Include(r => r.Training.Course)
            .Include(r => r.Training.Teacher)
            .Select(r => r.Training)
            .ToList();

        return View(myTrainings);
    }

    public ActionResult Create()
    {
        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi");
        ViewBag.CourseId = new SelectList(db.Courses, "Id", "Nimetus");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Training training)
    {
        if (ModelState.IsValid)
        {
            var course = new Course
            {
                Nimetus = training.CourseName,
                Keel = training.CourseKeel,
                Tase = training.CourseTase
            };

            db.Courses.Add(course);
            db.SaveChanges();

            training.Course = course;

            db.Trainings.Add(training);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi", training.TeacherId);
        return View(training);
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
    