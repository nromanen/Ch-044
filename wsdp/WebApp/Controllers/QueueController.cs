using System;
using System.Text;
using System.Web.Mvc;
using RabbitMQ.Client;
using System.Configuration;

namespace WebApp.Controllers
{ 
	public class QueueController : Controller
    {		
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
				
				var urls = new string[] { "http://rozetka.com.ua/ ", "http://hotline.ua/ ", "http://repka.ua/" };
				var message = string.Join(string.Empty,urls).ToCharArray();
                // the data put on the queue must be a byte array
                var data = Encoding.UTF8.GetBytes(message);
                // ensure that the queue exists before we publish to it
                channel.QueueDeclare("urls", false, false, false, null);
                // publish to the "default exchange", with the queue name as the routing key
                channel.BasicPublish("", "urls", null, data);
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
				channel.QueueDeclare("urls", false, false, false, null);
				// do a simple poll of the queue 
				var data = channel.BasicGet("urls", false);
				// the message is null if the queue was empty 
				if (data == null) return Json(null);
				// convert the message back from byte[] to a string
				var message = Encoding.UTF8.GetString(data.Body);
				// ack the message, ie. confirm that we have processed it
				// otherwise it will be requeued a bit later
				channel.BasicAck(data.DeliveryTag, false);
				return Json(message);
			}
		}
	}
}
