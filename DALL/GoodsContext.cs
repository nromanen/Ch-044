using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;
using System.Data.Entity.Migrations;

namespace DAL
{
    public class GoodsContext : DbContext
    {
        public GoodsContext() : base("name=DB2")
        {
            this.Configuration.LazyLoadingEnabled = false;


        }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>().HasKey(g => g.Id);
            modelBuilder.Entity<Producer>().HasKey(g => g.Id);
            modelBuilder.Entity<Category>().HasKey(g => g.Id);

            modelBuilder.Entity<Good>().HasRequired(g => g.Producer);
            modelBuilder.Entity<Good>().HasRequired(g => g.Category);
            modelBuilder.Entity<Producer>().HasMany(p => p.Goods).WithRequired(p => p.Producer);
            modelBuilder.Entity<Category>().HasMany(p => p.Goods).WithRequired(p => p.Category);

            base.OnModelCreating(modelBuilder);
        }
    }
}
