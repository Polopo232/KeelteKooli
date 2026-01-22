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
            .ToList();

        return View(trainings);
    }

    [Authorize(Roles = "Student")]
    public ActionResult Register(int id)
    {
        string userId = User.Identity.GetUserId();

        bool exists = db.Registrations.Any(r =>
            r.TrainingId == id &&
            r.ApplicationUserId == userId);

        if (exists)
        {
            TempData["Message"] = "Вы уже зарегистрированы на этот курс!";
            return RedirectToAction("Index");
        }

        db.Registrations.Add(new Registration
        {
            TrainingId = id,
            ApplicationUserId = userId,
            Staatus = "Pending"
        });

        db.SaveChanges();

        TempData["Message"] = "Вы успешно записаны на курс!";
        return RedirectToAction("MyCourses");
    }

    [Authorize(Roles = "Student")]
    public ActionResult MyCourses()
    {
        string userId = User.Identity.GetUserId();

        var myTrainings = db.Registrations
            .Where(r => r.ApplicationUserId == userId)
            .Include(r => r.Training.Course)
            .Include(r => r.Training.Teacher)
            .Select(r => r.Training)
            .ToList();

        return View(myTrainings);
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
