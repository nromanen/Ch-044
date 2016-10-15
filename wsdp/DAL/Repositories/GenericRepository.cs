using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories {
	public class GenericRepository<TEntity> : BaseRepository<TEntity>,
		IGenericRepository<TEntity> where TEntity : class {

		public GenericRepository(MainContext context)
			: base(context) {
		}
		public virtual TEntity GetByID(object id) {
			return dbSet.Find(id);
		}

		public virtual void Delete(object id) {
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}
	}
}
