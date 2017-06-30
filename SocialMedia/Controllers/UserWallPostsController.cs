using System;
using System.Net;
using System.Web.Mvc;
using SocialMedia.Models;
using Microsoft.AspNet.Identity;
using SocialMedia.DAL;

namespace SocialMedia.Controllers
{
    public class UserWallPostsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        public ActionResult Create(string postText)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = unitOfWork.Users.GetById(currentUserId);
            if (currentUser == null)
            {
                return HttpNotFound();
            }

            if (postText == "")
            {
                return RedirectToAction("Index", "User");
            }

            UserWallPost userPost = new UserWallPost() { Text = postText, PostedDate = DateTime.Now, UserId = currentUserId, User = currentUser };

            unitOfWork.UserWallPosts.Create(userPost);
            unitOfWork.Save();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unitOfWork.UserWallPosts.Delete(id);
            unitOfWork.Save();

            return RedirectToAction("Index","User");
        }
    }
}
