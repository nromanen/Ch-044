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
			var  user = new User() { Id = 1, Email = "admin@admin.admin", UserName = "admin", Password = "password", Role = role };
			var  user2 = new User() { Id = 2, Email = "user@admin.admin", UserName = "user", Password = "qwert22y", Role = role };
			var  user3 = new User() { Id = 3, Email = "user221@gmail.com", UserName = "user32", Password = "qwertyww33", Role = role_user };
			var  user4 = new User() { Id = 4, Email = "user113@hotmail.com", UserName = "user44", Password = "qwertywwwqw", Role = role_user };
			var  user5 = new User() { Id = 5, Email = "user22314@mail.ru", UserName = "user55", Password = "qwerty1423", Role = role_user };
			var  user6 = new User() { Id = 6, Email = "qwert@gmail.com", UserName = "user2231", Password = "qwertyqw123e11", Role = role_user };
			var  user7 = new User() { Id = 7, Email = "asdfg@outlook.com", UserName = "user4443", Password = "qw223rty", Role = role_user };
			var  user8 = new User() { Id = 8, Email = "qwerwtyt@gmail.com", UserName = "uoow123r", Password = "q22323werty", Role = role_user };
			var  user9 = new User() { Id = 9, Email = "useqwer@mail.ru", UserName = "userqwer", Password = "qqwerewerty", Role = role_user };
			var  user10 = new User() { Id = 10, Email = "useqqqr@gmail.com", UserName = "uwwq123r", Password = "q1123qwe", Role = role_user };
			var  user11 = new User() { Id = 11, Email = "qwerwss23342@gmail.ru", UserName = "ollw123e", Password = "11111231", Role = role_user };
			var user12 = new User() { Id = 12, Email = "user12@gmail.com", UserName = "qwretr2123", Password = "222233rty", Role = role_user };
			var  user13 = new User() { Id = 13, Email = "uqweqweer@mail.ru", UserName = "uqqqqq123", Password = "123123431", Role = role_user };
			var  user14 = new User() { Id = 14, Email = "qwretr@gmail.com", UserName = "qwer123t", Password = "222222432", Role = role_user };
			var  user15 = new User() { Id = 15, Email = "weeerr@qwert.admin", UserName = "uqweew2ser123", Password = "33531322", Role = role_user };
			var  user16 = new User() { Id = 16, Email = "wweew@homemail.com", UserName = "userino", Password = "q32523y", Role = role_user };
			var  user17 = new User() { Id = 17, Email = "user@admin.admin", UserName = "user123", Password = "qwert5y", Role = role_user };
			var user18 = new User() { Id = 18, Email = "us123eqqqr@gmail.com", UserName = "oooow", Password = "q11263qwe", Role = role_user };
			var user19 = new User() { Id = 19, Email = "qooow3342@gmail.ru", UserName = "111ollwe", Password = "111711", Role = role_user };
			var user20 = new User() { Id = 20, Email = "uwwwwe2@gmail.com", UserName = "qwret2223r2", Password = "222823rty", Role = role_user };
			var user21 = new User() { Id = 21, Email = "uqweq23weer@mail.ru", UserName = "uqq123qqq", Password = "1231231", Role = role_user };
			var user22 = new User() { Id = 22, Email = "qwret223r@gmail.com", UserName = "qwer11t", Password = "2222222", Role = role_user };
			var user23 = new User() { Id = 23, Email = "weee11rr@qwert.admin", UserName = "uqwe222ew2ser", Password = "334443", Role = role_user };
			var user24 = new User() { Id = 24, Email = "wwe11123ew@homemail.com", UserName = "userin333o", Password = "q3223y", Role = role_user };
			var user25 = new User() { Id = 25, Email = "us223er@admin.admin", UserName = "user44444", Password = "qwertqwey", Role = role_user };

			Category categorySmartPhonesAndMobilePhones = new Category() {
				Name = "SmartPhonesAndMobilePhones",
				OrderNo = 1
			};
			Category categoryForHouse = new Category() {
				Name = "ForHouse",
				OrderNo = 2
			};
			Category categoryComputers = new Category() {
				Name = "Computers",
				OrderNo = 3
			};
			Category categorySmartphones = new Category() {
				Name = "Smartphones",
				ParentCategory = categorySmartPhonesAndMobilePhones,
				OrderNo = 1
			};
			Category categoryMobilephones = new Category() {
				Name = "Mobile phones",
				ParentCategory = categorySmartPhonesAndMobilePhones,
				OrderNo = 2
			};
			Category categoryTv = new Category() {
				Name = "TV",
				ParentCategory = categoryForHouse,
				OrderNo = 1

			};
			Category categoryFridges = new Category() {
				Name = "Fridge",
				ParentCategory = categoryForHouse,
				OrderNo = 2
			};
			Category categoryMicrowaves = new Category() {
				Name = "Microwaves",
				ParentCategory = categoryForHouse,
				OrderNo = 3
			};
			Category categoryLaptops = new Category() {
				Name = "Laptops",
				ParentCategory = categoryComputers,
				OrderNo = 1
			};
			Property prop1 = new Property() {
				Category = categorySmartphones,
				Name = "Display",
				DefaultValue = "Retina",
				Description = "qwerty",
				Prefix = "mm",
				Sufix = "gg",
				Type = PropertyType.String
			};
			Property prop2 = new Property() {
				Category = categorySmartphones,
				Name = "Diagonal",
				DefaultValue = "5,7",
				Description = "qwerty",
				Prefix = "mm",
				Sufix = "gg",
				Type = PropertyType.String
			};
			Property prop3 = new Property() {
				Category = categorySmartphones,
				Name = "Camera",
				DefaultValue = "8",
				Description = "qwerty",
				Prefix = "px",
				Sufix = "px",
				Type = PropertyType.String
			};
			Property prop4 = new Property() {
				Category = categorySmartphones,
				Name = "Battery",
				DefaultValue = "3700",
				Description = "qwerty",
				Prefix = "amp",
				Sufix = "amp",
				Type = PropertyType.String
			};
			Property prop5 = new Property() {
				Category = categorySmartphones,
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
            Good good1 = new Good()
            {
                XmlData = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><PropertyValuesDTO type=\"Model.DTO.PropertyValuesDTO\"><DictStringProperties /><DictIntProperties /><DictDoubleProperties /></PropertyValuesDTO>",
                Category_Id = 4,
                WebShop_Id = 1,
                Status = true,
                Name = "Ноутбук Lenovo IdeaPad 110-17ACL (80UM002FRA) Black Суперцена!!!&nbsp;",
                Price = 7999,
                ImgLink = "http://i2.rozetka.ua/goods/1742927/lenovo_110_17acl_80um002fra_images_1742927925.jpg",
                UrlLink = "http://rozetka.com.ua/lenovo_110_17acl_80um002fra/p11781151/",
                OldPrice = 9999
            };
            PriceHistory priceHistory1 = new PriceHistory()
            {
                Name = "Lenovo ideapad",
                Url = "http://rozetka.com.ua/lenovo_110_17acl_80um002fra/p11781151/",
                Date = new DateTime(2016, 12, 1),
                Price = 9999
            };
            PriceHistory priceHistory2 = new PriceHistory()
            {
                Name = "Lenovo ideapad",
                Url = "http://rozetka.com.ua/lenovo_110_17acl_80um002fra/p11781151/",
                Date = new DateTime(2016, 12, 2),
                Price = 8888
            };
            //ParserTask task = new ParserTask() {
            //	Category = categorySmartphones,
            //	Description = "Smth",
            //	Priority = Priority.Low,
            //	Status = Status.NotFinished,
            //	WebShop = webShop1,
            //	EndDate = DateTime.Now,
            //             LastChange = DateTime.Now
            //         };
            //ParserTask task2 = new ParserTask() {
            //	Category = categorySmartphones,
            //	Description = "Smth",
            //	Priority = Priority.Middle,
            //	Status = Status.NotFinished,
            //	WebShop = webShop2,
            //	EndDate = DateTime.Now,
            //             LastChange = DateTime.Now
            //};

            context.Roles.AddOrUpdate(role_user);
            context.Users.AddOrUpdate(user);
            context.Users.AddOrUpdate(user2);
            context.Users.AddOrUpdate(user3);
            context.Users.AddOrUpdate(user4);
            context.Users.AddOrUpdate(user5);
            context.Users.AddOrUpdate(user6);
            context.Users.AddOrUpdate(user7);
            context.Users.AddOrUpdate(user8);
            context.Users.AddOrUpdate(user9);
            context.Users.AddOrUpdate(user10);
            context.Users.AddOrUpdate(user11);
            context.Users.AddOrUpdate(user12);
            context.Users.AddOrUpdate(user13);
            context.Users.AddOrUpdate(user14);
            context.Users.AddOrUpdate(user15);
            context.Users.AddOrUpdate(user16);
            context.Users.AddOrUpdate(user17);
            context.Users.AddOrUpdate(user18);
            //context.Users.AddOrUpdate(user19);
            //context.Users.AddOrUpdate(user20);
            //context.Users.AddOrUpdate(user21);
            //context.Users.AddOrUpdate(user22);
            //context.Users.AddOrUpdate(user23);
            //context.Users.AddOrUpdate(user24);
            //context.Users.AddOrUpdate(user25);
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
            //context.ParserTasks.AddOrUpdate(task);
            //context.ParserTasks.AddOrUpdate(task2);

            //context.Goods.AddOrUpdate(good1);
            //context.PriceHistory.AddOrUpdate(priceHistory1);
            //context.PriceHistory.AddOrUpdate(priceHistory2);
        }
	}
}