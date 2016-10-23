using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface {
	public interface IUnitOfWork {
		// IGenericRepository<User> UserRepo { get; }

        IGenericRepository<Good> GoodRepo { get; }
		IGenericRepository<Category> CategoryRepo { get; }
		void Dispose();
		void Save();
	}
}
