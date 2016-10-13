using DataAccessLogic;
using Goods.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GoodEFManager : IManager<Good>
    {
        UnitOfWork uof = new UnitOfWork();
        public void Create(Good item)
        {
            List<string> catsNames = uof.CategoryRepository.All.Select(i => i.Name).ToList();
            List<string> prodsNames = uof.ProducerRepository.All.Select(i => i.Name).ToList();
            List<string> prodsCountries = uof.ProducerRepository.All.Select(i => i.Country).ToList();
            List<string> goodsNames = uof.GoodRepository.All.Select(i => i.Name).ToList();
            var max = uof.GoodRepository.All.Select(i => i.Id).DefaultIfEmpty().Max();
            if (!goodsNames.Contains(item.Name))
            {
                if (max != 0)

                    item.Id = ++max;
                else
                {
                    max = 0;
                }
                if (catsNames.Contains(item.Category.Name))
                {
                    item.Category_Id = item.Category.Id;
                    item.Category = null;

                }
                else
                {
                    item.Category_Id = item.Category.Id;
                }
                if (prodsNames.Contains(item.Producer.Name) && prodsCountries.Contains(item.Producer.Country))
                {
                    item.Producer_Id = item.Producer.Id;
                    item.Producer = null;
                }
                else
                {
                    item.Producer_Id = item.Producer.Id;

                }
                uof.GoodRepository.Insert(item);
                uof.Save();
            }
        }

        public void Delete(Good item)
        {
            uof.GoodRepository.Delete(item);
            uof.Save();
        }

        public void Update(Good item)
        {
            uof.GoodRepository.Update(item);
            uof.Save();
        }

        public List<Good> GetAll()
        {
            return uof.GoodRepository.All.ToList();
        }

        public Good GetById(int id)
        {
            return uof.GoodRepository.GetByID(id);
        }
    }
}
