using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using Model.DTO;

namespace BAL.Interface
{
    public interface IGoodDatabasesWizard
    {
        void InsertOrUpdate(GoodDTO good);
        bool Delete(GoodDTO good);
        void Update(GoodDTO good);
    }
}
