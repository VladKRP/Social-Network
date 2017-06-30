using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Models;

namespace SocialMedia.DAL
{
    public class UserMessagesRepository:IRepository<UserMessage>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public UserMessagesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserMessage> GetAll()
        {
            return context.UserMessages;
        }

        public UserMessage GetById(int? id)
        {
            return context.UserMessages.Find(id);
        }

        public void Create(UserMessage message)
        {
            context.UserMessages.Add(message);
        }

        public void Update(UserMessage message)
        {
            context.Entry(message).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int? id)
        {
            UserMessage message = context.UserMessages.Find(id);
            if(message != null)
                context.UserMessages.Remove(message);
        }
    }
}
