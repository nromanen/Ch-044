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
        public void Add(Good item)
        {
            List<string> catsNames = uof.CategoryRepository.All.Select(i => i.Name).ToList();
            List<decimal> goodsPrices = uof.GoodRepository.All.Select(i => i.Price).ToList();
            List<string> prodsNames = uof.ProducerRepository.All.Select(i => i.Name).ToList();
            List<string> prodsCountries = uof.ProducerRepository.All.Select(i => i.Country).ToList();
            List<string> goodsNames = uof.GoodRepository.All.Select(i => i.Name).ToList();
            var maxg = uof.GoodRepository.All.Select(i => i.Id).DefaultIfEmpty().Max();
            var maxp = uof.ProducerRepository.All.Select(i => i.Id).DefaultIfEmpty().Max();
            var maxc = uof.CategoryRepository.All.Select(i => i.Id).DefaultIfEmpty().Max();
            if (!goodsNames.Contains(item.Name))
            {
                if (!goodsPrices.Contains(item.Price))
                {
                    if (maxg != 0)

                        item.Id = ++maxg;
                    else
                    {
                        maxg = 0;
                    }
                    if (catsNames.Contains(item.Category.Name))
                    {
                        item.Category_Id = item.Category.Id;
                        item.Category = null;
                    }
                    else
                    {
                        if (maxc != 0)

                            item.Category.Id = ++maxc;
                        else
                        {
                            maxc = 0;
                        }
                        item.Category_Id = item.Category.Id;
                    }
                    if (prodsNames.Contains(item.Producer.Name))
                    {
                        if (prodsCountries.Contains(item.Producer.Country))
                        {
                            item.Producer_Id = item.Producer.Id;
                            item.Producer = null;
                        }
                    }
                    else
                    {
                        if (maxp != 0)

                            item.Producer.Id = ++maxp;
                        else
                        {
                            maxp = 0;
                        }
                        item.Producer_Id = item.Producer.Id;

                    }
                    uof.GoodRepository.Insert(item);
                    uof.Save();
                }
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
