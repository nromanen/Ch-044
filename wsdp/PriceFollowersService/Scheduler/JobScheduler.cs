using BAL.Manager;
using DAL;
using Model.DTO;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceFollowersService.Scheduler
{
    public class JobScheduler : IJob
    {
        private UnitOfWork uOw = null;
        private FollowPriceManager followPriceManager = null;
        private PriceManager priceManager = null;
        public JobScheduler()
        {
            uOw = new UnitOfWork();
            followPriceManager = new FollowPriceManager(uOw);
            priceManager = new PriceManager(uOw);
        }
        public void Execute(IJobExecutionContext context)
        {
            var priceList = priceManager.GetAll();
            var followedPricesList = followPriceManager.GetAll();


            foreach (var item in followedPricesList)
            {
                var similarUrls = priceList.Where(u => u.Url == item.Url).OrderBy(d => d.Date).ToList();
                if (similarUrls.Count > 1)
                {
                    var firstDate = similarUrls[0];
                    var lastDate = similarUrls.Last();

                    if (lastDate.Date > firstDate.Date && lastDate.Price < firstDate.Price)
                    {
                        var price = lastDate.Price;

                        //manager.SendEmail(price, item.Email);
                        Console.WriteLine("Sended.");
                    }
                    else
                    {
                        Console.WriteLine("pizdec");
                    }
                }
            }
        }
    }
}

