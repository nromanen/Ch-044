using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	interface IRepository<TItem> where TItem: class  {
		TItem Get(int id);

		IEnumerable<TItem> GetAll();

		void Create(TItem item);

		void Update(TItem item);

		void Delete(int id);

		void DeleteAll();
	}
}
