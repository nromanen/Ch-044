using Model.DB;
using System.Data.Entity;

namespace DAL
{
    public class MainContext : DbContext
    {
        public MainContext()
            : base("OfficeSY")
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
    }
}