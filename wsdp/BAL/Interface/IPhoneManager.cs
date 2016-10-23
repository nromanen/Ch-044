using Model.DTO;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IPhoneManager
    {
        List<PhoneSimpleDTO> GetAllPhones();

        ConcreteGood GetPhoneById(int id);
    }
}
