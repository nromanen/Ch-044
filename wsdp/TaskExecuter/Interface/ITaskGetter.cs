using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace TaskExecuting.Interface
{
	public interface ITaskGetter
	{
		TaskExecuterModel GetTask();

	}
}
