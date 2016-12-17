using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceFollowersService.Scheduler
{
	public class PriceFollowersScheduler : BasicScheduler
	{
        public void Start()
        {
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<JobScheduler>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
	}
}
