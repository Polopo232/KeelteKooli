namespace KeelteKooli.Migrations
{
    using KeelteKooli.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            string[] roles = { "Admin", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            // ----- Teacher -----

            // 2. Создаём тестового пользователя
            var teacherEmail = "teacher@test.ee";
            var teacherUser = userManager.FindByEmail(teacherEmail);

            if (teacherUser == null)
            {
                teacherUser = new ApplicationUser
                {
                    UserName = teacherEmail,
                    Email = teacherEmail
                };

                userManager.Create(teacherUser, "Test123!");
            }

            // 3. Добавляем пользователя в роль Teacher
            if (!userManager.IsInRole(teacherUser.Id, "Teacher"))
            {
                userManager.AddToRole(teacherUser.Id, "Teacher");
            }

            // 4. Создаём запись в таблице Teachers
            var teacher = context.Teachers.FirstOrDefault(t => t.ApplicationUserId == teacherUser.Id);

            if (teacher == null)
            {
                teacher = new Teacher
                {
                    Nimi = "Test Õpetaja",
                    Kvalifikatsioon = "C1",
                    ApplicationUserId = teacherUser.Id
                };

                context.Teachers.Add(teacher);
                context.SaveChanges();
            }

            // 5. Создаём тестовый курс (Course)
            var course = context.Courses.FirstOrDefault(c => c.Nimetus == "Testkursus");

            if (course == null)
            {
                course = new Course
                {
                    Nimetus = "Testkursus",
                    Keel = "Inglise",
                    Tase = "A2"
                };

                context.Courses.Add(course);
                context.SaveChanges();
            }


            // 6. Создаём Training и привязываем к õpetaja
            var training = context.Trainings
                .FirstOrDefault(t => t.CourseId == course.Id && t.TeacherId == teacher.Id);

            if (training == null)
            {
                training = new Training
                {
                    CourseId = course.Id,
                    TeacherId = teacher.Id,
                    AlgusKuupaev = DateTime.Now.AddDays(7),
                    LoppKuupaev = DateTime.Now.AddMonths(1),
                    MaxOsalejaid = 10,
                    Hind = 120
                };

                context.Trainings.Add(training);
                context.SaveChanges();
            }
            // ----- Admin -----
            var adminUser = userManager.FindByEmail("admin@test.ee");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@test.ee",
                    Email = "admin@test.ee"
                };

                userManager.Create(adminUser, "Test123!");
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            // ----- Student -----
            var studentUser = userManager.FindByEmail("student@test.ee");
            if (studentUser == null)
            {
                studentUser = new ApplicationUser
                {
                    UserName = "student@test.ee",
                    Email = "student@test.ee"
                };

                userManager.Create(studentUser, "Test123!");
                userManager.AddToRole(studentUser.Id, "Student");
            }
        }
    }
}
