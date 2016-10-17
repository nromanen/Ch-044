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
        CategoryRepository(DbContext db) : base(db) { }



    }
}
