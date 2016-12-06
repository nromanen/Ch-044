using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskExecuting.PushScheduler
{
	public class PushJobScheduler
	{
		public static void Start()
		{
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
			scheduler.Start();

			IJobDetail job = JobBuilder.Create<PushJob>().Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(10)
						.RepeatForever())
				.Build();

			scheduler.ScheduleJob(job, trigger);
		}
	}
}