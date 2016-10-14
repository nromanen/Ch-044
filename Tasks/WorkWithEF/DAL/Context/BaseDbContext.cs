using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Mapping;
using Models;

namespace DAL.Context {
	public class BaseDbContext : DbContext {
		public BaseDbContext() : base("name=Connection") {
			this.Configuration.LazyLoadingEnabled = true;
		}
		static BaseDbContext() {
			Database.SetInitializer(new CreateDatabaseIfNotExists<BaseDbContext>());
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Producer> Producers { get; set; }
		public DbSet<Good> Goods { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Configurations.Add(new GoodMap());
			modelBuilder.Configurations.Add(new ProducerMap());
			modelBuilder.Configurations.Add(new CategoryMap());
		}

	}
}

