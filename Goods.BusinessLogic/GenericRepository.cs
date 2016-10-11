using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity;
using Goods.DbModels;

namespace Goods.BusinessLogic
{
    public class BaseGenericManager<T> where T : class
    {
        public List<T> All()
        {
            using (var context = new GoodsContext())
            {
                return context.Set<T>().ToList();
            }
        }
        public void Delete(T entity)
        {
            using (var context = new GoodsContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void Add(T newItem)
        {
            using (var context = new GoodsContext())
            {
                context.Set<T>().Add(newItem);
                context.SaveChanges();
            }
        }

        public T Get(int Id)
        {
            using (var context = new GoodsContext())
            {
                var res = context.Set<T>().Find(Id);
                return res;
            }
        }
        public void Update(T entity)
        {
            using (var context = new GoodsContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
