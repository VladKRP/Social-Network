using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SocialMedia.DAL;
using Microsoft.AspNet.Identity;
using PagedList;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private IEnumerable<Friend> IsConfirmRequest(bool isConfirmed)
        {
            var friends = unitOfWork.Friends.GetAll();
            friends = friends.Where(f => f.IsConfirmed == isConfirmed);
            return friends;
        }

        private PagedList<Friend> GenerateFriendList(IEnumerable<Friend> friends, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            PagedList<Friend> friendList = new PagedList<Friend>(friends.ToList(), pageNumber, pageSize);
            return friendList;
        }

        public ActionResult Index(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(true);
            friends = friends.Where((f => f.UserId == currentUserId || f.UserFriendId == currentUserId));
            return View(GenerateFriendList(friends,page));
        }

        public ActionResult IncomingRequests(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(false);
            friends = friends.Where((f => f.UserFriendId == currentUserId));
            return View(GenerateFriendList(friends, page));
        }

        public ActionResult OutgoingRequests(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var friends = IsConfirmRequest(false);
            friends = friends.Where((f => f.UserId == currentUserId));
            return View(GenerateFriendList(friends, page));
        }

        public ActionResult ConfirmRequest(string receiverId)
        {
            var currentUserId = User.Identity.GetUserId();
            if (receiverId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = unitOfWork.Users.GetById(receiverId);
            ApplicationUser userFriend = unitOfWork.Users.GetById(currentUserId);

            if (user != null && userFriend != null)
            {
                Friend friend = unitOfWork.Friends.GetAll().FirstOrDefault(f => f.UserId == receiverId && f.UserFriendId == currentUserId);
                friend.IsConfirmed = true;
                unitOfWork.Friends.Update(friend);
                unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = await unitOfWork.Friends.GetByIdAsync(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        public ActionResult Create(string id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = unitOfWork.Users.GetById(currentUserId);
            ApplicationUser userFriend = unitOfWork.Users.GetById(id);

            Friend friend = new Friend()
            {
                User = currentUser,
                UserId = currentUserId,
                UserFriend = userFriend,
                UserFriendId = id,
            };

            if (ModelState.IsValid)
            {
                unitOfWork.Friends.Create(friend);
                unitOfWork.Save();
            }
            return RedirectToAction("GetPeople","User");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
             unitOfWork.Friends.Delete(id);
             unitOfWork.Save();
             return RedirectToAction("Index");
        }

    }
}
