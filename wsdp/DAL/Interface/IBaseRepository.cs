using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface {
	public interface IBaseRepository<TEntity>
		where TEntity : class {
		IQueryable<TEntity> All { get; }

		IEnumerable<TEntity> Get (
			System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = ""
		);
		void Insert(TEntity entity);
		void Update(TEntity entityToUpdate);
		void SetStateModified(TEntity entity);
	}
}
