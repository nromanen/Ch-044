using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuting
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

        public static void SetCountOfThreads(int count)
        {
            //ConfigurationManager.AppSettings.Set("QuartzCountOfThreads", count.ToString());
            //ConfigurationManager.AppSettings.Add("QuartzCountOfThreads",count.ToString());
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["QuartzCountOfThreads"] == null)
                {
                    settings.Add("QuartzCountOfThreads", count.ToString());
                }
                else
                {
                    settings["QuartzCountOfThreads"].Value = count.ToString();
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }

            scheduler.Shutdown();

            CreateFactory();
            CreateScheduler();
        }
    }
}
