using DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace BusinessLogic
{
    public class ProducerEFManager : IManager<Producer>
    {
        UnitOfWork uof = new UnitOfWork();

        public void Create(Producer item)
        {
            uof.ProducerRepository.Insert(item);
            uof.Save();
        }

        public void Delete(Producer item)
        {
            uof.ProducerRepository.Delete(item);
            uof.Save();
        }

        public void Update(Producer item)
        {
            uof.ProducerRepository.Update(item);
            uof.Save();
        }

        public List<Producer> GetAll()
        {
            return uof.ProducerRepository.All.ToList();
        }

        public Producer GetById(int id)
        {
            return uof.ProducerRepository.GetByID(id);
        }
        public List<Good> GetGoodsOfProducer(int id)
        {
            var goods = uof.GoodRepository.All.Where(i => i.Producer.Id == id).ToList();

            return goods;
        }
    }
}
