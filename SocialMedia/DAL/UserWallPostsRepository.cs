using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMedia.Models;

namespace SocialMedia.DAL
{
    public class UserWallPostsRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public UserWallPostsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public UserWallPost GetById(int? id)
        {
            return context.UserWallPosts.Find(id);
        }

        public void Create(UserWallPost post)
        {
            context.UserWallPosts.Add(post);
        }

        public void Delete(int? id)
        {
            UserWallPost post = context.UserWallPosts.Find(id);
            if(post != null)
                context.UserWallPosts.Remove(post);
        }
    }
}