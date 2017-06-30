using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DAL
{
    interface IRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetAll();
        Entity GetById(int? id);
        void Create(Entity entity);
        void Update(Entity entity);
        void Delete(int? id);
    }
}
