using DataAccessLogic;
using Goods.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryEFManager : IManager<Category>
    {
        UnitOfWork uof = new UnitOfWork();

        public void Add(Category item)
        {

            List<string> catsNames = uof.CategoryRepository.All.Select(i => i.Name).ToList();
            var maxc = uof.CategoryRepository.All.Select(i => i.Id).DefaultIfEmpty().Max();
            if (!catsNames.Contains(item.Name))
            {
                item.Id = ++maxc;
                uof.CategoryRepository.Insert(item);
                uof.Save();
            }
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
