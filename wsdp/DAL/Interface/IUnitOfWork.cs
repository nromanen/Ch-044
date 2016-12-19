using Model.DB;

namespace DAL.Interface
{
	public interface IUnitOfWork
	{
		IGenericRepository<Role> RoleRepo { get; }
		IGenericRepository<User> UserRepo { get; }
		IGenericRepository<Good> GoodRepo { get; }
		IGenericRepository<Category> CategoryRepo { get; }
		IGenericRepository<Property> PropertyRepo { get; }
		IGenericRepository<WebShop> WebShopRepo { get; }
		IGenericRepository<ParserTask> ParserRepo { get; }
		IGenericRepository<PriceHistory> PriceRepo { get; }
		IGenericRepository<ExecutingInfo> ExecuteRepo { get; }
        IGenericRepository<AppSetting> AppSettingRepo { get; }
		IGenericRepository<PriceFollower> PriceFollowerRepo { get; }
		IGenericRepository<Comment> CommentRepo { get; }


		void Dispose();

		int Save();
	}
}