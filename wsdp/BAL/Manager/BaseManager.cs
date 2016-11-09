using DAL.Interface;
using log4net;

namespace BAL.Manager
{
    public class BaseManager
    {
        protected IUnitOfWork uOW;
        protected static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

        public BaseManager(IUnitOfWork uOW)
        {
            this.uOW = uOW;
        }
    }
}