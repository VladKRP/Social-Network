using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace SocialMedia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public DateTime? RegistrDate { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<UserWallPost> UserWallPosts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<UserWallPost> UserWallPosts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Logins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Claims");

        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>{

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