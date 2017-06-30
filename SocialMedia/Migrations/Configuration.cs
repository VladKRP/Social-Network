namespace SocialMedia.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SocialMedia.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);


            var admin = new ApplicationUser
            {
                Email = "adminmail@mail.ru",
                UserName = "adminmail@mail.ru",
                FirstName = "Mesh",
                LastName = "Look",
                Age = 19,
                Country = "Belarus",
                Gender = "male",
                RegistrDate = DateTime.Now
            };

            var testUser1 = new ApplicationUser
            {
                Email = "testmail1@mail.ru",
                UserName = "testmail1@mail.ru",
                FirstName = "Katrin",
                LastName = "Petters",
                Age = 29,
                Country = "UK",
                Gender = "female",
                RegistrDate = DateTime.Now
            };

            var testUser2 = new ApplicationUser
            {
                Email = "testmail2@mail.ru",
                UserName = "testmail2@mail.ru",
                FirstName = "Jonatan",
                LastName = "Fell",
                Age = 35,
                Country = "Germany",
                Gender = "male",
                RegistrDate = DateTime.Now
            };

            var testUser3 = new ApplicationUser
            {
                Email = "testmail3@mail.ru",
                UserName = "testmail3@mail.ru",
                FirstName = "Fall",
                LastName = "Hero",
                Age = 20,
                Country = "Spain",
                Gender = "female",
                RegistrDate = DateTime.Now
            };

            string testPassword = "3535690q";

            List<ApplicationUser> users = new List<ApplicationUser>() { admin, testUser1, testUser2, testUser3 };

            foreach (var user in users)
            {
                var result = userManager.Create(user, testPassword);

                if (result.Succeeded)
                {
                    if (user.FirstName == "Mesh")
                        userManager.AddToRole(user.Id, adminRole.Name);
                    userManager.AddToRole(user.Id, userRole.Name);
                }
            }

            base.Seed(context);
        }
    }
}
