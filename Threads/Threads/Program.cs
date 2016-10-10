using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {

        static void Main(string[] args)
        {
            string pathToDirectory = "d:/softserve/Task2";

            var manager = new Manager();

            var pathes = manager.GetFiles(pathToDirectory);
            manager.GetThreadList(pathes);
            manager.ThreadsWorker(pathToDirectory);


            Console.ReadLine();

        }

    }

}
