using DAL.Interface;
using DAL.Repositories;
using Model.DB;
using System;
using System.Data.Entity.Validation;

namespace DAL
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private MainContext context;

		#region Private Repositories

		private IGenericRepository<User> userRepo;
		private IGenericRepository<Good> goodRepo;
		private IGenericRepository<Category> categoryRepo;
		private IGenericRepository<Property> propertyRepo;
		private IGenericRepository<WebShop> webShopRepo;
		private IGenericRepository<Role> roleRepo;
		private IGenericRepository<ParserTask> parserRepo;
		private IGenericRepository<PriceHistory> priceRepo;
		private IGenericRepository<ExecutingInfo> executeRepo;
        private IGenericRepository<AppSetting> appSettingsRepo;
		private IGenericRepository<PriceFollower> priceFollowerRepo;
		private IGenericRepository<Comment> commentRepo;

		#endregion Private Repositories

		public UnitOfWork()
		{
			context = new MainContext();

			roleRepo = new GenericRepository<Role>(context);
			userRepo = new GenericRepository<User>(context);
			goodRepo = new GenericRepository<Good>(context);
			categoryRepo = new GenericRepository<Category>(context);
			propertyRepo = new GenericRepository<Property>(context);
			webShopRepo = new GenericRepository<WebShop>(context);
			parserRepo = new GenericRepository<ParserTask>(context);
			priceRepo = new GenericRepository<PriceHistory>(context);
			executeRepo = new GenericRepository<ExecutingInfo>(context);
            appSettingsRepo = new GenericRepository<AppSetting>(context);
			priceFollowerRepo = new GenericRepository<PriceFollower>(context);
			commentRepo = new GenericRepository<Comment>(context);


		}

		#region Repositories Getters
		public IGenericRepository<PriceHistory> PriceRepo
		{
			get
			{
				if (priceRepo == null) priceRepo = new GenericRepository<PriceHistory>(context);
				return priceRepo;
			}
		}

		public IGenericRepository<Role> RoleRepo
		{
			get
			{
				if (roleRepo == null) roleRepo = new GenericRepository<Role>(context);
				return roleRepo;
			}
		}

		public IGenericRepository<User> UserRepo
		{
			get
			{
				if (userRepo == null) userRepo = new GenericRepository<User>(context);
				return userRepo;
			}
		}

		public IGenericRepository<Good> GoodRepo
		{
			get
			{
				if (goodRepo == null) goodRepo = new GenericRepository<Good>(context);
				return goodRepo;
			}
		}

		public IGenericRepository<Category> CategoryRepo
		{
			get
			{
				if (categoryRepo == null) categoryRepo = new GenericRepository<Category>(context);
				return categoryRepo;
			}
		}

		public IGenericRepository<Property> PropertyRepo
		{
			get
			{
				if (propertyRepo == null) propertyRepo = new GenericRepository<Property>(context);
				return propertyRepo;
			}
		}

		public IGenericRepository<WebShop> WebShopRepo
		{
			get
			{
				if (webShopRepo == null) webShopRepo = new GenericRepository<WebShop>(context);
				return webShopRepo;
			}
		}

		public IGenericRepository<ParserTask> ParserRepo
		{
			get
			{
				if (parserRepo == null) parserRepo = new GenericRepository<ParserTask>(context);
				return parserRepo;
			}
		}

		public IGenericRepository<ExecutingInfo> ExecuteRepo {
			get {
				if (executeRepo == null)
					executeRepo = new GenericRepository<ExecutingInfo>(context);
				return executeRepo;
			}
		}

        public IGenericRepository<AppSetting> AppSettingRepo
        {
            get
            {
                if (appSettingsRepo == null)
                    appSettingsRepo = new GenericRepository<AppSetting>(context);
                return appSettingsRepo;
            }
        }

		public IGenericRepository<PriceFollower> PriceFollowerRepo
		{
			get
			{
				if (priceFollowerRepo == null)
					priceFollowerRepo = new GenericRepository<PriceFollower>(context);
				return priceFollowerRepo;
			}
		}

		public IGenericRepository<Comment> CommentRepo {
			get {
				if (commentRepo == null)
					commentRepo = new GenericRepository<Comment>(context);
				return commentRepo;
			}
		}

		#endregion Repositories Getters

		public void UpdateContext()
	    {
	        context = new MainContext();
	    }
		public int Save()
		{
			try
			{
				return context.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
			    return 0;
			}
		}

		#region Dispose

		// https://msdn.microsoft.com/ru-ru/library/system.idisposable(v=vs.110).aspx

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion Dispose
	}
}