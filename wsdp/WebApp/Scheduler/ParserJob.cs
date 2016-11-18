using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using RabbitMQ.Client;
using System.Text;
using Model.DTO;
using System.Web.Script.Serialization;
using BAL.Interface;
using DAL;
using BAL.Manager;
using WebApp.Models;

namespace WebApp.Scheduler
{
	public class ParserJob : IJob
	{
		private UnitOfWork uOw = null;
		private ParserTaskManager parsermanager = null;
		public ParserJob()
		{
			uOw = new UnitOfWork();
			parsermanager = new ParserTaskManager(uOw);

		}
		public void Execute(IJobExecutionContext context)
		{

			ConnectionFactory connFactory = new ConnectionFactory();
			connFactory.uri = new Uri("amqp://bpmcftle:cxjupG82CztHJ_Nfkh2GUEyb0Z-2FyGY@chicken.rmq.cloudamqp.com/bpmcftle");
			// create a connection and open a channel, dispose them when done
			using (var conn = connFactory.CreateConnection())
			using (var channel = conn.CreateModel())
			{
				channel.QueueDeclare(queue: "json",
					 durable: true,
					 exclusive: false,
					 autoDelete: false,
					 arguments: null);
				var taskList = new List<TaskExecuterModel>();
				var taskid = parsermanager.GetAll().Select(i => i.Id);
				foreach (var i in taskid)
				{
					var task = new TaskExecuterModel();
					task.Id = i;
					taskList.Add(task);
				}
				var serializer = new JavaScriptSerializer();
				var output = serializer.Serialize(taskList).ToCharArray();

				//var message = string.Join("*", model).ToCharArray();
				// the data put on the queue must be a byte array
				var data = Encoding.UTF8.GetBytes(output);
				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;
				// ensure that the queue exists before we publish to it
				//channel.QueueDeclare("json", false, false, false, null);
				// publish to the "default exchange", with the queue name as the routing key
				channel.BasicPublish(exchange: "",
					 routingKey: "json",
					 basicProperties: properties,
					 body: data);
				//channel.BasicPublish("", "json", null, data);
			}

		}
	}
}