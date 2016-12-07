using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuting.Scheduler
{
	public class TaskExecutingScheduler
	{
		private static ISchedulerFactory factory;
		private static IScheduler scheduler;

		static TaskExecutingScheduler()
		{
			CreateFactory();
			CreateScheduler();
		}

		private static void CreateFactory()
		{
			int countOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["QuartzCountOfThreads"]);

			NameValueCollection properties = new NameValueCollection();
			properties["quartz.threadPool.threadCount"] = "1";

			factory = new StdSchedulerFactory(properties);
		}

		private static void CreateScheduler()
		{
			scheduler = factory.GetScheduler();
		}

		public static void SetCountOfThreadsAndRestart(int count)
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

			Start();
		}


		public static void Start()
		{
			scheduler.Start();

			IJobDetail job = JobBuilder.Create<ExecuterJob>().Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(1)
						.RepeatForever())
				.Build();

			scheduler.ScheduleJob(job, trigger);
		}
	}
}
