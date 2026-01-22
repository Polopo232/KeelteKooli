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

            var teacherUser = userManager.FindByEmail("teacher@test.ee");

            if (teacherUser == null)
            {
                teacherUser = new ApplicationUser
                {
                    UserName = "teacher@test.ee",
                    Email = "teacher@test.ee"
                };

                userManager.Create(teacherUser, "Test123!");
                userManager.AddToRole(teacherUser.Id, "Teacher");
            }

            if (!context.Teachers.Any(t => t.ApplicationUserId == teacherUser.Id))
            {
                context.Teachers.Add(new Teacher
                {
                    Nimi = "Test Õpetaja",
                    Kvalifikatsioon = "C1",
                    ApplicationUserId = teacherUser.Id
                });

                context.SaveChanges();
            }
        }

    }
}
