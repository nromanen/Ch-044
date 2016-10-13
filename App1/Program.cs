using BusinessLogic;
using Goods.DbModels;
using Multithreading;
using System;
using DataAccessLogic;
using Goods.Managers;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CategoryPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Categories.csv";
            const string ProducerPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Producers.csv";
            const string GoodsPath = @"C:\Users\Слава\Desktop\softserve ac\App1\files\Goods.csv";
            const string GoodsXmlPath = @"C:\Users\Слава\Desktop\GitRep\Ch-044\files\Goods.xml";
            const string GoodsXmlPath2 = @"C:\Users\Слава\Desktop\GitRep\Ch-044\files\Goods2.xml";
            string[] PathesofDir = new string[] { @"C:\Users\Слава\Desktop\softserve ac\App1\files\", @"C:\Users\Слава\Desktop\C#", @"C:\Users\Слава\Desktop\Новая папка" };
            try
            {
                //ParseManager parser = new ParseManager();

                //var patheses = parser.GetPathes(@"C:\Users\Слава\Desktop\Taxi-master", @"*.cs");

                //parser.ManageThreadWork(patheses, 5, "!=");
                //CategoryEFManager categorymanager = new CategoryEFManager();
                //var cat1 = categorymanager.Get(2, cont);
                //var prod = producermanager.Get(1, cont);
                //GoodEFManager goodmang = new GoodEFManager();
                //var tempobj = goodmang.Get(10);
                //var www = goodmang.All();
                // producermanager.All();

                //tempobj.Category = cat1;
                //goodmang.Update(tempobj, cont);

                ManagerXmlLinq manxml = new ManagerXmlLinq();
                var items = manxml.ReadFromFile(GoodsXmlPath);
                var items2 = manxml.ReadFromFile(GoodsXmlPath2);
                GoodEFManager goodman = new GoodEFManager();
                foreach (var i in items)
                {
                    goodman.Create(i);
                }
                foreach (var i in items2)
                {
                    goodman.Create(i);
                }
                Console.WriteLine("Done!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadKey();
        }
        static void Method(List<Good> collection)
        {
        }
    }
}

