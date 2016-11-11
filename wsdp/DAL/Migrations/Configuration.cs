using Model.DB;

namespace DAL.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	using Common.Enum;
	internal sealed class Configuration : DbMigrationsConfiguration<DAL.MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.MainContext context)
        {
            //  This method will be called after migrating to the latest version.
            Role role = new Role() { Id = 1, Name = "Administrator", Description = "Application administrator" };
            Role role_user = new Role() { Id = 2, Name = "User", Description = "Application user" };
            User user = new User() { Id = 1, Email = "admin@admin.admin", UserName = "admin", Password = "password", Role = role };
            User user2 = new User() { Id = 2, Email = "user@admin.admin", UserName = "user", Password = "qwerty", Role = role };

            Category categorySmartPhonesAndMobilePhones = new Category()
            {
                Name = "SmartPhonesAndMobilePhones",
                OrderNo = 1
            };
            Category categoryForHouse = new Category()
            {
                Name = "ForHouse",
                OrderNo = 2
            };
            Category categoryComputers = new Category()
            {
                Name = "Computers",
                OrderNo = 3
            };
            Category categorySmartphones = new Category()
            {
                Name = "Smartphones",
                ParentCategory = categorySmartPhonesAndMobilePhones,
                OrderNo = 1
            };
            Category categoryMobilephones = new Category()
            {
                Name = "Mobile phones",
                ParentCategory = categorySmartPhonesAndMobilePhones,
                OrderNo = 2
            };
            Category categoryTv = new Category()
            {
                Name = "TV sets",
                ParentCategory = categoryForHouse,
                OrderNo = 1

            };
            Category categoryFridges = new Category()
            {
                Name = "Fridges",
                ParentCategory = categoryForHouse,
                OrderNo = 2
            };
            Category categoryMicrowaves = new Category()
            {
                Name = "Microwaves",
                ParentCategory = categoryForHouse,
                OrderNo = 3
            };
            Category categoryLaptops = new Category()
            {
                Name = "Laptops",
                ParentCategory = categoryComputers,
                OrderNo = 1
            };
			Property prop1 = new Property() {
				Category = categorySmartPhonesAndMobilePhones,
				Name = "Display",
				DefaultValue = "Retina",
				Description = "qwerty",
				Prefix = "mm",
				Sufix = "gg",
				Type = PropertyType.String
			};
			Property prop2 = new Property() {
				Category = categorySmartPhonesAndMobilePhones,
				Name = "Diagonal",
				DefaultValue = "5,7",
				Description = "qwerty",
				Prefix = "mm",
				Sufix = "gg",
				Type = PropertyType.String
			};
			Property prop3 = new Property() {
				Category = categorySmartPhonesAndMobilePhones,
				Name = "Camera",
				DefaultValue = "8",
				Description = "qwerty",
				Prefix = "px",
				Sufix = "px",
				Type = PropertyType.String
			};
			Property prop4 = new Property() {
				Category = categorySmartPhonesAndMobilePhones,
				Name = "Battery",
				DefaultValue = "3700",
				Description = "qwerty",
				Prefix = "amp",
				Sufix = "amp",
				Type = PropertyType.String
			};
			Property prop5 = new Property() {
				Category = categorySmartPhonesAndMobilePhones,
				Name = "Sound",
				DefaultValue = "Gromkiy",
				Description = "qwerty",
				Prefix = "mm",
				Sufix = "gg",
				Type = PropertyType.String
			};
			Model.DB.WebShop webShop1 = new Model.DB.WebShop() {
				Name = "Rozetka",
				Path = "http://rozetka.com.ua",
				Status = true
			};
			Model.DB.WebShop webShop2 = new Model.DB.WebShop() {
				Name = "Foxtrot",
				Path = "http://foxtrot.com.ua",
				Status = true
			};
			ParserTask task = new ParserTask() {
				Category = categorySmartPhonesAndMobilePhones,
				Description = "Smth",
				Priority = "High",
				Status = "Not Finished",
				WebShop = webShop1,
				EndDate = DateTime.Now
			};
			ParserTask task2 = new ParserTask() {
				Category = categorySmartPhonesAndMobilePhones,
				Description = "Smth",
				Priority = "High",
				Status = "Not Finished",
				WebShop = webShop2,
				EndDate = DateTime.Now
			};


            context.Roles.AddOrUpdate(role);
            context.Roles.AddOrUpdate(role_user);
            context.Users.AddOrUpdate(user);
            context.Users.AddOrUpdate(user2);
            context.Categories.AddOrUpdate(categorySmartPhonesAndMobilePhones);
            context.Categories.AddOrUpdate(categoryForHouse);
            context.Categories.AddOrUpdate(categoryComputers);
            context.Categories.AddOrUpdate(categorySmartphones);
            context.Categories.AddOrUpdate(categoryMobilephones);
            context.Categories.AddOrUpdate(categoryTv);
            context.Categories.AddOrUpdate(categoryFridges);
            context.Categories.AddOrUpdate(categoryMicrowaves);
            context.Categories.AddOrUpdate(categoryLaptops);
			context.WebShops.AddOrUpdate(webShop1);
			context.WebShops.AddOrUpdate(webShop2);
			context.Properties.AddOrUpdate(prop1);
			context.Properties.AddOrUpdate(prop2);
			context.Properties.AddOrUpdate(prop3);
			context.Properties.AddOrUpdate(prop4);
			context.Properties.AddOrUpdate(prop5);
			context.ParserTasks.AddOrUpdate(task);
			context.ParserTasks.AddOrUpdate(task2);
		}
    }
}