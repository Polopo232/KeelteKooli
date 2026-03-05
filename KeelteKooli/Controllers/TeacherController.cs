using KeelteKooli.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public class TeacherController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [Authorize(Roles = "Admin")]
    public ActionResult Dashboard()
    {
        var teachers = db.Teachers.ToList();
        return View(teachers);
    }

    [HttpGet]
    public ActionResult CreateProfile()
    {
        ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateProfile(Teacher teacher, HttpPostedFileBase upload)
    {
        if (ModelState.IsValid)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/Teachers/"), fileName);

                upload.SaveAs(path);

                teacher.FotoPath = fileName;
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "UserName");
        return View(teacher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id)
    {
        var teacher = db.Teachers.Find(id);
        if (teacher != null)
        {
            if (!string.IsNullOrEmpty(teacher.FotoPath))
            {
                string fullPath = Server.MapPath("~/Images/Teachers/" + teacher.FotoPath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            db.Teachers.Remove(teacher);
            db.SaveChanges();
        }
        return RedirectToAction("Dashboard");
    }

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
