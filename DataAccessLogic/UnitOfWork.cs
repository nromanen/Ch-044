using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace DataAccessLogic
{
    public class UnitOfWork : IDisposable
    {
        private GoodsContext context = new GoodsContext();
        private BaseRepository<Category> categoryRepository;
        private BaseRepository<Producer> producerRepository;
        private BaseRepository<Good> goodRepository;

        public BaseRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new BaseRepository<Category>(context);
                }
                return categoryRepository;
            }
        }
        public BaseRepository<Producer> ProducerRepository
        {
            get
            {
                if (this.producerRepository == null)
                {
                    this.producerRepository = new BaseRepository<Producer>(context);
                }
                return producerRepository;

            }
        }
        public BaseRepository<Good> GoodRepository
        {
            get
            {
                if (this.goodRepository == null)
                {
                    this.goodRepository = new BaseRepository<Good>(context);
                }
                return goodRepository;

            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

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
    }
}
