using BAL;
using BAL.Manager;
using DAL;
using Model.DTO;
using SiteProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecuting.Interface;
using TaskExecuting.Manager;
using TaskExecuting.PushScheduler;
using TaskExecuting.Scheduler;

namespace TaskExecuting
{
	class Program
	{

		static void Main(string[] args)
		{
            PushJobScheduler.Start();

            TaskExecutingScheduler.Start();

            //TaskExecuter te = new TaskExecuter();

            //te.ExecuteTask(3, "http://www.foxtrot.com.ua/ru/shop/noutbuki_hp_15-ay080ur-x8p85ea.html");

			Console.ReadLine();
		}
	}
}
