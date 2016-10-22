using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IPhoneManager
    {
        void ParseGoodsFromCategory(string urlpath);
        List<PhoneSimpleDTO> GetAllPhones();
    }
}
