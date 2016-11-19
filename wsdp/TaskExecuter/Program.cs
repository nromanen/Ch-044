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
using TaskExecuting.Scheduler;

namespace TaskExecuting
{
	class Program
	{

		static void Main(string[] args)
		{
			TaskExecutingScheduler.Start();

			Console.ReadLine();
		}
	}
}
