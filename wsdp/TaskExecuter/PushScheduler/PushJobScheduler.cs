using Quartz;

namespace TaskExecuting.PushScheduler
{
    public class PushJobScheduler : BasicScheduler
	{
		public static void Start()
		{
			scheduler.Start();

			IJobDetail job = JobBuilder.Create<PushJob>().Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(100)
						.RepeatForever())
				.Build();

			scheduler.ScheduleJob(job, trigger);
		}
	}
}