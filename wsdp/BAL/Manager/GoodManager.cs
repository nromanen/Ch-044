using BAL.Interface;
using DAL.Interface;
using Model.DB;

namespace BAL.Manager
{
    public class GoodManager : BaseManager, IGoodManager
    {
        public GoodManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public void InsertGood(Good good)
        {
            uOW.GoodRepo.Insert(good);
            uOW.Save();
        }
    }
}