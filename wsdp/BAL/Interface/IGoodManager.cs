using Model.DB;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IGoodManager
    {
        void InsertGood(Good good);
        List<Good> GetAll();
    }
}