using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using RabbitMQ.Client;
using System.Text;
using System.Web.Script.Serialization;
using DAL;
using BAL.Manager;
using TaskExecuting.Models;

namespace TaskExecuting.PushScheduler
{
    public class PushJob : IJob
	{
		private UnitOfWork uOw = null;
		private ParserTaskManager parserManager = null;
		private URLManager urlManager = null;
		public PushJob()
		{
			uOw = new UnitOfWork();
			parserManager = new ParserTaskManager(uOw);
			urlManager = new URLManager(uOw);
		}
		/// <summary>
		/// Publishing messeges to rabbitmq queue
		/// </summary>
		/// <param name="context"></param>
		public void Execute(IJobExecutionContext context)
		{
			var taskList = new List<TaskExecuterModel>();
			var tasklistdb = parserManager.GetAll().Where(i => i.Status == (Common.Enum.Status.Coming) || (i.Status==Common.Enum.Status.Infinite)).ToList();
			foreach (var task in tasklistdb)
			{
				var urlList = urlManager.GetAllUrls(task.IteratorSettings);
				foreach (var url in urlList)
				{
					var taskExecute = new TaskExecuterModel();
					taskExecute.TaskId = task.Id;
					taskExecute.GoodUrl = url;
					taskList.Add(taskExecute);
				}

				break;
			}

			foreach (var mess in taskList)
			{
				ConnectionFactory connFactory = new ConnectionFactory();
				connFactory.uri = new Uri(System.Configuration.ConfigurationManager.AppSettings["RabbitMqConnection"]);
				// create a connection and open a channel, dispose them when done
				using (var conn = connFactory.CreateConnection())
				using (var channel = conn.CreateModel())
				{
					channel.QueueDeclare(queue:"Queue-"+Environment.MachineName,
						 durable: true,
						 exclusive: false,
						 autoDelete: false,
						 arguments: null);
					var serializer = new JavaScriptSerializer();
					var output = serializer.Serialize(mess).ToCharArray();
					// the data put on the queue must be a byte array
					var data = Encoding.UTF8.GetBytes(output);
					var properties = channel.CreateBasicProperties();
					properties.Persistent = true;
					// ensure that the queue exists before we publish to it
					// publish to the "default exchange", with the queue name as the routing key
					channel.BasicPublish(exchange: "",
						 routingKey: "Queue-" + Environment.MachineName,
						 basicProperties: properties,
						 body: data);
				}
			}
			var ids_update = taskList.Select(i => i.TaskId).Distinct();
			foreach (var id in ids_update)
			{
				var obj=parserManager.Get(id);
				obj.Status = Common.Enum.Status.InQuery;
				parserManager.Update(obj);
			}
		}
	}
}