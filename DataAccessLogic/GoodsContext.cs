using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;
using System.Data.Entity;

namespace DataAccessLogic
{
    public class GoodsContext : DbContext
    {
        public GoodsContext() : base("name=DB2")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
