using BAL.Manager;
using DAL;
using Quartz;
using System;
using System.Configuration;

namespace TaskExecuting.PushScheduler
{
    public class PushJobScheduler : BasicScheduler
	{
        public UnitOfWork uOw = null;
        public AppSettingsManager appSettingsManager = null;

        public PushJobScheduler()
        {
            uOw = new UnitOfWork();
            appSettingsManager = new AppSettingsManager(uOw);
        }

        public void Start()
		{
            int interval = appSettingsManager.Get().PushInterval;
            if (interval == 0) interval = 10;

            scheduler.Start();

			IJobDetail job = JobBuilder.Create<PushJob>().Build();

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