using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using SocialMedia.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using PagedList;
using SocialMedia.DAL;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetById(currentUserId);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        public ActionResult GetPeople()
        {
            return View();
        }

        public PartialViewResult GetPeoplePartial(string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }

            var currentUserId = User.Identity.GetUserId();
            var friends1 = unitOfWork.Friends.GetAll().Select(u => new { userId = u.UserId, user = u.UserFriend }).Where(u => u.userId == currentUserId);
            var friends2 = unitOfWork.Friends.GetAll().Select(u => new { userId = u.UserFriendId, user = u.User }).Where(u => u.userId == currentUserId);
            var currentUserFriends = friends1.Union(friends2).Select(u => u.user);
            var users = unitOfWork.Users.GetAll().Where(u => u.Id != currentUserId).Except(currentUserFriends);

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString)
                || u.LastName.Contains(searchString));
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            PagedList<ApplicationUser> userList = new PagedList<ApplicationUser>(users.ToList(), pageNumber, pageSize);
            return PartialView(userList);
        }

     
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("Index", user);
        }

        public FileContentResult GetImage(string userId)
        {
            ApplicationUser user = unitOfWork.Users.GetById(userId);
            if (user != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public ActionResult ChangeUserPhoto(HttpPostedFileBase image)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser user = unitOfWork.Users.GetById(currentUserId);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (image != null)
            {
                 user.ImageMimeType = image.ContentType;
                 user.ImageData = new byte[image.ContentLength];
                 image.InputStream.Read(user.ImageData, 0, image.ContentLength);
                 unitOfWork.Users.Update(user);
                 unitOfWork.Save();
            }

            return RedirectToAction("Index");

        }
    }
}