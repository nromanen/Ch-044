using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Product;
using DAL.Interface;
using log4net;
using Model.DB;
using ExtendedXmlSerialization;
using AutoMapper;

namespace BAL.Manager
{
    public class PhoneManager : BaseManager, IPhoneManager
    {

        public PhoneManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public List<PhoneSimpleDTO> GetAllPhones()
        {
            List<PhoneSimpleDTO> phones = new List<PhoneSimpleDTO>();
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            foreach (var phoneDb in uOW.GoodRepo.All.Where(g => g.Category == Common.Enum.GoodCategory.Phone))
            {
                var phone = ser.Deserialize(phoneDb.XmlData, typeof(ConcreteGood)) as ConcreteGood;
                phone.Id = phoneDb.Id;
                phones.Add(Mapper.Map<PhoneSimpleDTO>(phone));
            }

            return phones;
        }

        public ConcreteGood GetPhoneById(int id)
        {
            Good good = null;
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            try
            {
                good = uOW.GoodRepo.All.Where(g => g.Id == id).First();
                if (good == null)
                    throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            var phone = ser.Deserialize(good.XmlData, typeof(ConcreteGood)) as ConcreteGood;
            phone.Id = id;
            return phone;
        }

        public void ParseGoodsFromCategory(string urlpath)
        {
            throw new NotImplementedException();
        }
    }
}
