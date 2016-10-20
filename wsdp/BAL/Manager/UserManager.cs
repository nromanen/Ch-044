using BAL.Interface;
using DAL.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public UserManager(IUnitOfWork uOW)
            : base(uOW)
        {

        }
      
    }
}
