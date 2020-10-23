using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService.Services
{
    public class EntityAsyncService<T> : ICrudAsyncService<T> where T : class
    {
        public async Task CreateAsync(T entity)
        {
            using (var context = new Context())
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Set<T>().Find(id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<T> ReadAsync(int id)
        {
            using (var context = new Context())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            using (var context = new Context())
            {
                await Task.Delay(5000);
                return await context.Set<T>().ToListAsync();
            }
        }

        public async Task UpdateAsync(T person)
        {
            using (var context = new Context())
            {
                context.Set<T>().Attach(person);
                context.Entry(person).State = System.Data.Entity.EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
