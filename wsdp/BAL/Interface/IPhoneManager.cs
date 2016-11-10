using Model.DTO;
using Model.Product;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IPhoneManager
    {
        List<PhoneSimpleDTO> GetAllPhones();

        ConcreteGood GetPhoneById(int id);
    }
}