using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository {
	public interface IRepository<T> where T : class {
		T Get(int id);

		IEnumerable<T> GetAll();
		bool Create(T item);
		void Update(T item);
		bool Delete(int id);
	}
}
