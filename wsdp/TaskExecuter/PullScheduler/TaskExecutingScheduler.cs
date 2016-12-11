using BAL.Manager;
using DAL;
using Quartz;
using System;
using System.Configuration;

namespace TaskExecuting.Scheduler
{
    public class TaskExecutingScheduler : BasicScheduler
    {
        public UnitOfWork uOw = null;
        public AppSettingsManager appSettingsManager=null;

        public TaskExecutingScheduler()
        {
            uOw = new UnitOfWork();
            appSettingsManager = new AppSettingsManager(uOw);
        }
		public void Start()
		{
            int interval = appSettingsManager.Get().PullInterval;
            if (interval == 0) interval = 1;
            scheduler.Start();

			IJobDetail job = JobBuilder.Create<ExecuterJob>().Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(interval)
						.RepeatForever())
				.Build();

			scheduler.ScheduleJob(job, trigger);
		}
	}
}
