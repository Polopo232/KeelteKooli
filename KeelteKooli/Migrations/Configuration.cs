namespace KeelteKooli.Migrations
{
    using KeelteKooli.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KeelteKooli.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KeelteKooli.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string[] roles = { "Admin", "Teacher", "Student" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role)) roleManager.Create(new IdentityRole(role));
            }

            CreateUserIfNotExists(userManager, "admin@test.ee", "Admin");
            CreateUserIfNotExists(userManager, "student@test.ee", "Student");

            var seedData = new List<(string Name, string Email, string Qual, string CourseName, string Lang, string Level, decimal Price)>
            {
                ("John Smith", "smith@test.ee", "Master of Arts", "English for Beginners", "Inglise", "A1", 150),
                ("Emily Johnson", "johnson@test.ee", "PhD in Linguistics", "Business English", "Inglise", "C1", 250),
                ("Michael Brown", "brown@test.ee", "TEFL Certified", "German Intermediate", "Saksa", "B1", 180),
                ("Sarah Davis", "davis@test.ee", "Language Expert", "French Basics", "Prantsuse", "A2", 160),
                ("Robert Wilson", "wilson@test.ee", "Native Speaker", "Spanish Conversation", "Hispaania", "B2", 200),
                ("Jennifer Miller", "miller@test.ee", "C2 Expert", "Estonian for Foreigners", "Eesti", "A1", 120)
            };

            foreach (var item in seedData)
            {
                CreateTeacherAndTraining(context, userManager, item);
            }

            context.SaveChanges();
        }

        private void CreateUserIfNotExists(UserManager<ApplicationUser> userManager, string email, string role)
        {
            var user = userManager.FindByEmail(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email };
                userManager.Create(user, "Test123!");
                userManager.AddToRole(user.Id, role);
            }
        }

        private void CreateTeacherAndTraining(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            (string Name, string Email, string Qual, string CourseName, string Lang, string Level, decimal Price) data)
        {
            var user = userManager.FindByEmail(data.Email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = data.Email, Email = data.Email };
                userManager.Create(user, "Test123!");
                userManager.AddToRole(user.Id, "Teacher");
            }

            var teacher = context.Teachers.FirstOrDefault(t => t.ApplicationUserId == user.Id);
            if (teacher == null)
            {
                teacher = new Teacher { Nimi = data.Name, Kvalifikatsioon = data.Qual, ApplicationUserId = user.Id };
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }

            var course = context.Courses.FirstOrDefault(c => c.Nimetus == data.CourseName);
            if (course == null)
            {
                course = new Course { Nimetus = data.CourseName, Keel = data.Lang, Tase = data.Level };
                context.Courses.Add(course);
                context.SaveChanges();
            }

            if (!context.Trainings.Any(t => t.CourseId == course.Id && t.TeacherId == teacher.Id))
            {
                context.Trainings.Add(new Training
                {
                    CourseId = course.Id,
                    TeacherId = teacher.Id,
                    AlgusKuupaev = DateTime.Now.AddDays(14),
                    LoppKuupaev = DateTime.Now.AddMonths(3),
                    MaxOsalejaid = 12,
                    Hind = data.Price
                });
            }
        }
    }
}