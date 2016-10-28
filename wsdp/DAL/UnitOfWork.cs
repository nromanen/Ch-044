using DAL.Interface;
using DAL.Repositories;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion

        public UnitOfWork()
        {
            context = new MainContext();

            userRepo = new GenericRepository<User>(context);
            goodRepo = new GenericRepository<Good>(context);
            categoryRepo = new GenericRepository<Category>(context);
            propertyRepo = new GenericRepository<Property>(context);
        }

        #region Repositories Getters

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

        #endregion

        public void Save()
        {
            context.SaveChanges();
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

        #endregion
    }
}
