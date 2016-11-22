using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuting.Scheduler
{
	public class TaskExecutingScheduler
	{
		public static void Start()
		{

			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
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
