using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SocialMedia.Models;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SocialMedia.DAL;

namespace SocialMedia.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        private ApplicationUserManager _userManager;
        private UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationDbContext context = new ApplicationDbContext();

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetPeopleList(string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }

            var users = unitOfWork.Users.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString)
                || u.LastName.Contains(searchString));
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return PartialView("PeopleListPartial",new PagedList<ApplicationUser>(users.ToList(), pageNumber, pageSize));
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(context.Roles,"Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName,
                    Age = model.Age, Gender = model.Gender, Country = model.Country, Email = model.Email, RegistrDate = DateTime.Now };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.Roles);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Roles = new SelectList(context.Roles, "Name", "Name");
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,Country,Gender,Email, UserName, RegistrDate, PasswordHash, SecurityStamp , ImageData, ImageMimeType")] ApplicationUser user)
        {
            
            if (ModelState.IsValid)
            {
                unitOfWork.Users.Update(user);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        private void DeleteUserDependencies(IQueryable entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ApplicationUser user)
        {
            var friends = context.Friends.Where(u => u.UserId == user.Id || u.UserFriendId == user.Id);
            var messages = context.UserMessages.Where(u => u.ReceiverId == user.Id || u.SenderId == user.Id);
            var dialogs = context.Dialogs.Where(u => u.ReceiverId == user.Id || u.SenderId == user.Id);
            var wallPost = context.UserWallPosts.Where(u => u.UserId == user.Id);

            DeleteUserDependencies(friends);
            DeleteUserDependencies(messages);
            DeleteUserDependencies(dialogs);
            DeleteUserDependencies(wallPost);

            context.Entry(user).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
