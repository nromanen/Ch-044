using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.IRepositories;
using System.Data.Entity;

namespace ByEntity.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(GoodContext db) : base(db) { }
        private GoodContext Db { get { return (GoodContext)Context; } }

        public Category GetByName(string name)
        {
            return Db.Categories.FirstOrDefault(x => x.Name == name);    
            
        }

        public Category GetByUnique(string name, string country)
        {
            return Db.Categories.FirstOrDefault(x => x.Name == name);

        }






    }
}
