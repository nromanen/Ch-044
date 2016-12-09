using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecuting.Interface;
using System.Web.Script.Serialization;
//using System.Web.Helpers;
using Newtonsoft.Json;
using TaskExecuting.Models;

namespace TaskExecuting.Manager
{
	public class TaskGetter : ITaskGetter
	{
		public TaskExecuterModel GetTask()
		{
			ConnectionFactory connFactory = new ConnectionFactory();
			connFactory.uri = new Uri(System.Configuration.ConfigurationManager.AppSettings["RabbitMqConnection"]);
			using (var conn = connFactory.CreateConnection())
			using (var channel = conn.CreateModel())
			{

				// ensure that the queue exists before we access it
				//channel.QueueDeclare("json", false, false, false, null);
				// do a simple poll of the queue 
				var data = channel.BasicGet("Queue-" + Environment.MachineName, false);
				// the message is null if the queue was empty 

				if (data == null)
				{
					Console.WriteLine("Queue is empty!There are no Tasks!");
				}
				else
				{
					// convert the message back from byte[] to a string
					var message = Encoding.UTF8.GetString(data.Body);
					// ack the message, ie. confirm that we have processed it
					// otherwise it will be requeued a bit later
					channel.BasicAck(data.DeliveryTag, false);
					var task = JsonConvert.DeserializeObject<TaskExecuterModel>(message);
					return task;
				}
				return null;

			}
		}
	}
}
