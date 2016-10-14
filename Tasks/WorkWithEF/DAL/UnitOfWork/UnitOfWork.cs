using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Repository;

namespace DAL.UnitOfWork {
	public class UnitOfWork : IDisposable {
		public BaseDbContext db = new BaseDbContext();
		private GoodRepository goodRepository;
		private CategoryRepository categoryRepository;
		private ProducerRepository producerRepository;

		public GoodRepository Goods {
			get {
				if (goodRepository == null)
					goodRepository = new GoodRepository(db);
				return goodRepository;
			}
		}

		public CategoryRepository Categories {
			get {
				if (categoryRepository == null)
					categoryRepository = new CategoryRepository(db);
				return categoryRepository;
			}
		}

		public ProducerRepository Producers {
			get {
				if (producerRepository == null)
					producerRepository = new ProducerRepository(db);
				return producerRepository;
			}
		}

		public void Save() {
				db.SaveChanges();
		}

		private bool disposed = false;

		public virtual void Dispose(bool disposing) {
			if (!this.disposed) {
				if (disposing) {
					db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
