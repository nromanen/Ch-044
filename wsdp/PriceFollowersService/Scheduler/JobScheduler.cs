using BAL.Manager;
using DAL;
using Model.DTO;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
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
		private EmailService emailService = null;
		public JobScheduler()
		{
			uOw = new UnitOfWork();
			followPriceManager = new FollowPriceManager(uOw);
			priceManager = new PriceManager(uOw);
			emailService = new EmailService(uOw);
		}
		public void Execute(IJobExecutionContext context)
		{
			var email = ConfigurationManager.AppSettings["Email"];
			var pass = ConfigurationManager.AppSettings["Password"];

			var priceList = priceManager.GetAll();
			var followedPricesList = followPriceManager.GetAll();

			foreach (var item in followedPricesList)
			{
				var good = uOw.GoodRepo.GetByID(item.Good_Id);
				if (good != null)
				{
					var lastprice = priceList.Where(u => u.Url == good.UrlLink).OrderBy(d => d.Date).Select(i => i.Price).Last();
					if (lastprice < item.Price)
					{
						item.Price = lastprice;
						followPriceManager.Update(item);    
						var model = item;

						bool result = emailService.SendEmail(model, item.Price, email, pass);
						if (result == true)
						{
							item.Status = Common.Enum.FollowStatus.Sended;
							followPriceManager.Update(item);

                            //Console.WriteLine($"Message about price fall for {good.Name} was sended.");
						}
						else
						{
                            //Console.WriteLine($"Message for {good.Name} wasn't sended.");

						}
					}
				}

			}
		}
	}
}

