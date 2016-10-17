using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;

namespace ByEntity.Repositories
{
    class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository(GoodContext db) : base(db) { }
        public GoodContext Db { get { return (GoodContext)Context; } }

        //public Good GetByName(string name)
        //{
        //    return Db.Goods.FirstOrDefault(x => x.Name == name);

        //}
    }
}
