using System;
using System.Text;
using System.Web.Mvc;
using RabbitMQ.Client;
using System.Configuration;
using BAL.Interface;
using System.Linq;
using Model.DTO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class QueueController : Controller
	{
		private IParserTaskManager parserManager { get; }
		public QueueController(IParserTaskManager parserManager)
		{
			this.parserManager = parserManager;
		}
		public ActionResult Index()
		{

			return View();
		}

		[HttpPost]
		public ActionResult Publish()
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
				var taskid = parserManager.GetAll().Select(i => i.Id);
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
			return new EmptyResult();
		}

		public ActionResult Get()
		{
			ConnectionFactory connFactory = new ConnectionFactory();
			connFactory.uri = new Uri("amqp://bpmcftle:cxjupG82CztHJ_Nfkh2GUEyb0Z-2FyGY@chicken.rmq.cloudamqp.com/bpmcftle");
			using (var conn = connFactory.CreateConnection())
			using (var channel = conn.CreateModel())
			{
				// ensure that the queue exists before we access it
				//channel.QueueDeclare("json", false, false, false, null);
				// do a simple poll of the queue 
				var data = channel.BasicGet("json", false);
				// the message is null if the queue was empty 
				if (data == null) return Json(null);
				// convert the message back from byte[] to a string
				var message = Encoding.UTF8.GetString(data.Body);
				// ack the message, ie. confirm that we have processed it
				// otherwise it will be requeued a bit later
				channel.BasicAck(data.DeliveryTag, false);
				var serializer = new JavaScriptSerializer();
				return Json(message);
			}
		}
	}
}
