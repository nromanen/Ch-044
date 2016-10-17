using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.Repositories;

namespace ByEntity.DbServices
{
    class GoodServices
    {
        private GoodRepository _repository;

        public GoodServices(GoodContext db)
        {
            _repository = new GoodRepository(db);
        }

        public int AddList(List<Good> list)
        {         
            CategoryServices catServices = new CategoryServices(_repository.Db);
            List<Category> categories = list.Select(x => new Category { Name = x.Category.Name }).ToList();
            catServices.AddList(categories);            

            ProducerServices prodServices = new ProducerServices(_repository.Db);
            List<Producer> producers = list.Select(x => new Producer { Name = x.Producer.Name, Country = x.Producer.Country }).ToList();
            prodServices.AddList(producers);

            categories = new CategoryRepository(_repository.Db).GetAll().ToList();
            producers = new ProducerRepository(_repository.Db).GetAll().ToList();


            foreach (var item in list)
            {
                _repository.Add(
                    new Good
                    {
                        Name = item.Name,
                        Price = item.Price,
                        CategoryId = categories.Find(x => x.Name == item.Category.Name).Id,
                        ProducerId = producers.Find(x => x.Name == item.Producer.Name ).Id
                    });                         

            }

            
            return list.Count;
        }
    }
}
