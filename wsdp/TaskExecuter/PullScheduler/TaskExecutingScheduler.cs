using Quartz;

namespace TaskExecuting.Scheduler
{
    public class TaskExecutingScheduler : BasicScheduler
    {
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
