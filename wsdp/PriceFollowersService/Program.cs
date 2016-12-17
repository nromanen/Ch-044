using BAL;
using PriceFollowersService.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceFollowersService
{
	class Program
	{
		static void Main(string[] args)
        {
            AutoMapperConfig.Configure();
            var priceFollowerScheduler = new PriceFollowersScheduler();
            priceFollowerScheduler.Start();

            Console.ReadLine();
		}
	}
}
