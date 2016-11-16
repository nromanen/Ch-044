using BAL;
using BAL.Manager;
using DAL;
using Model.DTO;
using SiteProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecuting.Interface;

namespace TaskExecuting
{
    class Program
    {
      
        static void Main(string[] args)
        {
            AutoMapperConfig.Configure();
            UnitOfWork uOw = new UnitOfWork();
            ParserTaskManager pm = new ParserTaskManager(uOw);

            ParserTaskDTO task = pm.Get(1);

            Console.WriteLine(task.Description);

            Console.ReadLine();
        }
    }
}
