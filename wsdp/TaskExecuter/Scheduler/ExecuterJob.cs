using BAL.Manager;
using DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecuting.Manager;

namespace TaskExecuting.Scheduler
{
	public class ExecuterJob : IJob
	{
		private UnitOfWork uOw = null;
		private ParserTaskManager parsermanager = null;
		public ExecuterJob()
		{
			uOw = new UnitOfWork();
			parsermanager = new ParserTaskManager(uOw);

		}
		public void Execute(IJobExecutionContext context)
		{
			TaskExecuter te = new TaskExecuter();
			TaskGetter tg = new TaskGetter();

			var obj = tg.GetTask();
			var endTime = parsermanager.Get(obj.TaskId).EndDate;
			if(endTime==null)
			{
				te.ExecuteTask(obj.TaskId, obj.GoodUrl);
				var task_s = parsermanager.Get(obj.TaskId);
				task_s.Status = (Common.Enum.Status.Infinite);
				parsermanager.Update(task_s);
			}
			else if (endTime != null && DateTime.Now <= endTime)
			{
				te.ExecuteTask(obj.TaskId, obj.GoodUrl); }
			else
			{
				var task_s = parsermanager.Get(obj.TaskId);
				task_s.Status = (Common.Enum.Status.Finished);
				parsermanager.Update(task_s);
			}

		}
	}
}
