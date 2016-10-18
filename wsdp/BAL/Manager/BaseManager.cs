using DAL.Interface;
using log4net;
using Model.DB;
using Model.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class BaseManager
    {
        protected IUnitOfWork uOW;
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public BaseManager(IUnitOfWork uOW)
        {
            this.uOW = uOW;
        }
    }
}
