using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Goods.DbModels;



namespace DataAccess
{
    public class GoodsContext:DbContext
    {
            public GoodsContext() : base("name=DB2")
            {
                this.Configuration.LazyLoadingEnabled = false;
            }
            public DbSet<Good> Goods { get; set; }
            public DbSet<Producer> Producers { get; set; }
            public DbSet<Category> Categories { get; set; }
    }
 }
