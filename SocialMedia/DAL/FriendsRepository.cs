using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMedia.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    public class FriendsRepository:IRepository<Friend>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public FriendsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Friend> GetAll()
        {
            return context.Friends.Include(f => f.User).Include(f => f.UserFriend);
        }

        public Friend GetById(int? id)
        {
            return context.Friends.Find(id);
        }

        public async Task<Friend> GetByIdAsync(int? id)
        {
            return  await context.Friends.FindAsync(id);
        }

        public void Create(Friend friend)
        {
            context.Friends.Add(friend);
        }

        public void Update(Friend friend)
        {
            context.Entry(friend).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Friend friend = context.Friends.Find(id);
            if(friend != null)
                context.Friends.Remove(friend);
        }


    }
}