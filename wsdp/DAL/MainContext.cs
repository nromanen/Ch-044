using Model.DB;
using System.Data.Entity;

namespace DAL
{
	public class MainContext : DbContext
	{
		public MainContext()
			: base("MyShop")
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public MainContext(string connString)
			: base(connString)
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Good> Goods { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<WebShop> WebShops { get; set; }
		public DbSet<Property> Properties { get; set; }
		public DbSet<ParserTask> ParserTasks { get; set; }
		public DbSet<PriceHistory> PriceHistory { get; set; }
		public DbSet<ExecutingInfo> ExecutingInfo { get; set; }
		public DbSet<AppSetting> AppSettings { get; set; }
		public DbSet<PriceFollower> PriceFollowers { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}