using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    partial class ShopContextEf : DbContext
    {
        public ShopContextEf()
            : base("EfConnection")
        {
            List<Producer> pp = this.Producers.ToList();
            List<Category> cc = this.Categories.ToList();
            List<Good> gg = this.Goods.ToList();
            List<Good> testg = pp.SelectMany(p => p.Goods).ToList();
            List<Good> testgg = cc.SelectMany(p => p.Goods).ToList(); 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Good> Goods { get; set; }

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
