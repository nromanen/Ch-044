using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.Repositories;

namespace ByEntity.DbServices
{
    class ProducerServices
    {
        private ProducerRepository _repository;

        public ProducerServices(GoodContext db)
        {
            _repository = new ProducerRepository(db);
        }

        public int AddList(List<Producer> list)
        {            
            List<Producer> listToDb = list.GroupBy(x => x.Name).Select(x => x.First()).ToList();
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
