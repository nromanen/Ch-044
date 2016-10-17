using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.Repositories;
using ByEntity.IRepositories;

namespace ByEntity.DbServices
{
    class CategoryServices
    {        
        private CategoryRepository _repository;

        public CategoryServices(GoodContext db)
        {
            _repository = new CategoryRepository(db);
        }

        public int AddList(List<Category> list)
        {            
            //List<Category> listFromDb = _repository.GetAll().ToList();
            List<Category> listToDb = list.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            int counter = 0; 
            foreach (var item in listToDb)
            {
                if (_repository.GetByName(item.Name) == null)
                {
                    _repository.Add(item);
                    counter++;                                   
                }

            }
            return counter;
        }

       
        
    }
}
