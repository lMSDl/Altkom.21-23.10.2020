using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService.Services
{
    public class EntityService<T> : ICrudService<T> where T : class
    {
        public void Create(T entity)
        {
            using (var context = new Context())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Set<T>().Find(id);
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public T Read(int id)
        {
            using (var context = new Context())
            {
                return context.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> Read()
        {
            using (var context = new Context())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Update(T person)
        {
            using (var context = new Context())
            {
                context.Set<T>().Attach(person);
                context.Entry(person).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
