using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Goods.DbModels;
using Goods.Managers;
using Goods.BusinessLogic;
using Multithreading;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CategoryPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Categories.csv";
            const string ProducerPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Producers.csv";
            const string GoodsPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Goods.csv";
            const string GoodsXmlPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Goods.xml";
            string[] PathesofDir = new string[] { @"C:\Users\Слава\Desktop\softserve ac\App1\files\", @"C:\Users\Слава\Desktop\C#",@"C:\Users\Слава\Desktop\Новая папка" };
            try
            {
                ParseCsManager parser = new ParseCsManager();

                var patheses=parser.GetPathes(@"C:\Users\Слава\Desktop\softserve ac\App1");
                parser.GetThreadList(patheses);
                parser.ManageThreadWork(patheses);


                ProducerEFManager gef = new ProducerEFManager();
                var a=gef.Get(1);




                Console.WriteLine("Done!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadKey();
        }
    }
    }

