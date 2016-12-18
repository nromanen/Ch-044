using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IEmailService
    {
        bool SendEmail(PriceFollowerDTO model, decimal? price, string email, string password);
    }
}
