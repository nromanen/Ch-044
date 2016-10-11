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
        public DbContext GoodsContext { get; set; }

        public List<T> All(DbContext GoodsContext)
        {
            return GoodsContext.Set<T>().ToList();
        }
        public void Delete(T entity, DbContext GoodsContext)
        {
            GoodsContext.Entry(entity).State = EntityState.Deleted;
            GoodsContext.SaveChanges();
        }
        public void Add(T newItem, DbContext GoodsContext)
        {
            GoodsContext.Set<T>().Add(newItem);
            GoodsContext.SaveChanges();
        }

        public void AddList(List<T> collection, DbContext GoodsContext)
        {
            foreach (var item in collection)
            {
                GoodsContext.Set<T>().Add(item);
                GoodsContext.SaveChanges();

            }
        }
        public T Get(int Id, DbContext GoodsContext)
        {
            var res = GoodsContext.Set<T>().Find(Id);
            return res;
        }
        public void Update(T entity, DbContext GoodsContext)
        {
            GoodsContext.Entry(entity).State = EntityState.Modified;
            GoodsContext.SaveChanges();
        }

    }
}
