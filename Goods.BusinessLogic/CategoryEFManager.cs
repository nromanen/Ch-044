using DataAccessLogic;
using Goods.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryEFManager : IManager<Category>
    {
        UnitOfWork uof = new UnitOfWork();

        public void Create(Category item)
        {
            uof.CategoryRepository.Insert(item);
            uof.Save();
        }

        public void Delete(Category item)
        {
            uof.CategoryRepository.Delete(item);
            uof.Save();
        }

        public void Update(Category item)
        {
            uof.CategoryRepository.Update(item);
            uof.Save();
        }

        public List<Category> GetAll()
        {
            return uof.CategoryRepository.All.ToList();
        }

        public Category GetById(int id)
        {
            return uof.CategoryRepository.GetByID(id);
        }
    }
}
