using KeelteKooli.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
        var userId = User.Identity.GetUserId();
        var myCourses = db.Trainings
            .Include(t => t.Course)
            .Include(t => t.Teacher)
            .Include(t => t.Registrations)
            .Where(t => t.Registrations.Any(r => r.ApplicationUserId == userId))
            .ToList();

        return View(myCourses);
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

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi");
        return View();
    }

    // GET: Trainings/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Training training = db.Trainings.Find(id);
        if (training == null) return HttpNotFound();

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi", training.TeacherId);
        return View(training);
    }

    // POST: Trainings/Edit/5
    // POST: Trainings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Training training)
    {
        if (ModelState.IsValid)
        {
            var trainingInDb = db.Trainings
                                 .Include(t => t.Course)
                                 .FirstOrDefault(t => t.Id == training.Id);

            if (trainingInDb != null)
            {
                trainingInDb.TeacherId = training.TeacherId;
                trainingInDb.AlgusKuupaev = training.AlgusKuupaev;
                trainingInDb.LoppKuupaev = training.LoppKuupaev;
                trainingInDb.Hind = training.Hind;
                trainingInDb.MaxOsalejaid = training.MaxOsalejaid;

                if (trainingInDb.Course != null)
                {
                    trainingInDb.Course.Nimetus = training.CourseName;
                    trainingInDb.Course.Keel = training.CourseKeel;
                    trainingInDb.Course.Tase = training.CourseTase;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi", training.TeacherId);
        return View(training);
    }

    // GET: Trainings/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var training = db.Trainings.Include(t => t.Course).Include(t => t.Teacher).FirstOrDefault(t => t.Id == id);

        if (training == null) return HttpNotFound();

        return View(training);
    }

    // POST: Trainings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Training training = db.Trainings.Find(id);

        // Если нужно удалить и сам курс из таблицы Courses:
        // var course = db.Courses.Find(training.CourseId);
        // if (course != null) db.Courses.Remove(course);

        db.Trainings.Remove(training);
        db.SaveChanges();
        return RedirectToAction("Index");
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
    