using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz.Impl;
using System.Configuration;

namespace PriceFollowersService
{
    abstract public class BasicScheduler
    {
        protected static ISchedulerFactory factory;
        protected static IScheduler scheduler;

        static BasicScheduler()
        {
            CreateFactory();
            CreateScheduler();
        }

        protected static void CreateFactory()
        {
            int countOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["QuartzCountOfThreads"]);
            NameValueCollection properties = new NameValueCollection();

            properties["quartz.threadPool.threadCount"] = countOfThreads.ToString();
            factory = new StdSchedulerFactory(properties); 
        }

        protected static void CreateScheduler()
        {
            scheduler = factory.GetScheduler();
        }
    }
}
