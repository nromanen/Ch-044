using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByEntity.Model
{
    public class GoodContext : DbContext
    {
        public GoodContext(string connection = "DefaultConnection") : base(connection) { }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
         
    }
}
