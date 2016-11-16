using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using RabbitMQ.Client;
using System.Text;

namespace WebApp.Scheduler
{
	public class ParserJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			ConnectionFactory connFactory = new ConnectionFactory();
			connFactory.uri = new Uri("amqp://bpmcftle:cxjupG82CztHJ_Nfkh2GUEyb0Z-2FyGY@chicken.rmq.cloudamqp.com/bpmcftle");
			// create a connection and open a channel, dispose them when done
			using (var conn = connFactory.CreateConnection())
			using (var channel = conn.CreateModel())
			{

				var urls = new string[] { "http://rozetka.com.ua/ ", "http://hotline.ua/ ", "http://repka.ua/" };
				var message = string.Join(string.Empty, urls).ToCharArray();
				// the data put on the queue must be a byte array
				var data = Encoding.UTF8.GetBytes(message);
				// ensure that the queue exists before we publish to it
				channel.QueueDeclare("urls", false, false, false, null);
				// publish to the "default exchange", with the queue name as the routing key
				channel.BasicPublish("", "urls", null, data);
			}

		}
	}
}