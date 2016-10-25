using Model.DB;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(DAL.MainContext context) {
			//  This method will be called after migrating to the latest version.
			Role role = new Role() { Id = 1, Name = "Administrator", Description = "Application administrator"};
			User user = new User() { Id = 1, Email = "admin@admin.admin", UserName = "admin", Password = "password", Role = role };

			context.Roles.AddOrUpdate(role);
			context.Users.AddOrUpdate(user);
		}
	}
}
