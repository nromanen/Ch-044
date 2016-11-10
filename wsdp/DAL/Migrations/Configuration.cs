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

        protected override void Seed(DAL.MainContext context)
        {
            //  This method will be called after migrating to the latest version.
            Role role = new Role() { Id = 1, Name = "Administrator", Description = "Application administrator" };
            Role role_user = new Role() { Id = 2, Name = "User", Description = "Application user" };
            User user = new User() { Id = 1, Email = "admin@admin.admin", UserName = "admin", Password = "password", Role = role };

            Category categorySmartPhonesAndMobilePhones = new Category()
            {
                Name = "SmartPhonesAndMobilePhones"
            };
            Category categoryForHouse = new Category()
            {
                Name = "ForHouse"
            };
            Category categoryComputers = new Category()
            {
                Name = "Computers"
            };
            Category categorySmartphones = new Category()
            {
                Name = "Smartphones",
                ParentCategory = categorySmartPhonesAndMobilePhones
            };
            Category categoryMobilephones = new Category()
            {
                Name = "Mobile phones",
                ParentCategory = categorySmartPhonesAndMobilePhones
            };
            Category categoryTv = new Category()
            {
                Name = "TV sets",
                ParentCategory = categoryForHouse
            };
            Category categoryFridges = new Category()
            {
                Name = "Fridges",
                ParentCategory = categoryForHouse
            };
            Category categoryMicrowaves = new Category()
            {
                Name = "Microwaves",
                ParentCategory = categoryForHouse
            };
            Category categoryLaptops = new Category()
            {
                Name = "Laptops",
                ParentCategory = categoryComputers
            };

            Model.DB.WebShop webShop1 = new Model.DB.WebShop()
            {
                Name = "Rozetka",
                Path = "http://rozetka.com.ua",
                Status = true
            };
            //context.Roles.AddOrUpdate(role);
            //context.Users.AddOrUpdate(user);
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
        }
    }
}
