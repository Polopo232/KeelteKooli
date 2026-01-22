using System.Linq;
using System.Web.Mvc;
using KeelteKooli.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Dashboard()
    {
        string userId = User.Identity.GetUserId();

        var teacher = db.Teachers
            .Include(t => t.Trainings.Select(tr => tr.Registrations.Select(r => r.User)))
            .Include(t => t.Trainings.Select(tr => tr.Course))
            .FirstOrDefault(t => t.ApplicationUserId == userId);

        return View(teacher);
    }
}
