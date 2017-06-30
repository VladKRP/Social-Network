using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMedia.Models;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    public class UsersRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public UsersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Users;
        }

        public ApplicationUser GetById(string id)
        {
            return context.Users.Find(id);
        }

        public void Update(ApplicationUser user)
        {
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int? id)
        {
            ApplicationUser user = context.Users.Find(id);
            if (user != null)
                context.Users.Remove(user);
        }


    }
}