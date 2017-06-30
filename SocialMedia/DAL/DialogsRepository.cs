using System.Collections.Generic;
using SocialMedia.Models;
using System.Data.Entity;

namespace SocialMedia.DAL
{
    public class DialogsRepository:IRepository<Dialog>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public DialogsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Dialog> GetAll()
        {
            return context.Dialogs.Include(d => d.Receiver).Include(d => d.Sender).Include(d => d.Messages);
        }

        public Dialog GetById(int? id)
        {
            return context.Dialogs.Find(id);
        }

        public void Create(Dialog dialog)
        {
            context.Dialogs.Add(dialog);
        }

        public void Update(Dialog dialog)
        {
            context.Entry(dialog).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Dialog dialog = context.Dialogs.Find(id);
            if (dialog != null)
            {
                context.Dialogs.Remove(dialog);
            }
        }
    }
}