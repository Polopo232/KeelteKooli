using System.Linq;
using System.Web.Mvc;
using KeelteKooli.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;


public class TeacherController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [Authorize(Roles = "Admin")]
    public ActionResult Dashboard()
    {
        var teachers = db.Teachers.ToList();
        return View(teachers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(teacher);
    }

    // 1. Открывает пустую форму (GET)
    [HttpGet]
    public ActionResult CreateProfile()
    {
        ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
        return View();
    }

    // 2. Обрабатывает нажатие кнопки "Salvesta" (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateProfile(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        // Если была ошибка (например, имя пустое), возвращаем список пользователей снова
        ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
        return View(teacher);
    }

    // Удалите старый метод ActionResult Create(Teacher teacher), он больше не нужен

    public ActionResult CreateTraining()
    {
        ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateTraining(Training training)
    {
        if (ModelState.IsValid)
        {
            training.Course = new Course { Nimetus = training.CourseName };
            db.Trainings.Add(training);
            db.SaveChanges();
            return RedirectToAction("Index", "Trainings");
        }

        ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Nimi", training.TeacherId);
        return View(training);
    }
}
