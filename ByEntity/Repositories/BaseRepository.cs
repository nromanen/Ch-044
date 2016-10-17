using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.IRepositories;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace ByEntity.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        public DbSet<T> DbSet { get; private set; }

        public BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            if(item == null)
            {
                Console.WriteLine("OPs");
                return;
            }
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public bool Delete(T item)
        {
            T res;             
            try
            {
                res = DbSet.Remove(item);
            }
            catch(EntityException ex)
            {
                throw ex;
            }

            Context.SaveChanges();
            return res != null;            
        }
       

        public T Get(object id)
        {
            return DbSet.Find(id);
        }

        public ICollection<T> GetAll()
        {
            return DbSet.ToList();
        }

        public bool Update(T updated, object key)
        {      
                  
            if (updated == null)
                return false;

            T existing = DbSet.Find(key);
            bool res = existing != null;
            if (res)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            
            return res;
        }
    }
}
