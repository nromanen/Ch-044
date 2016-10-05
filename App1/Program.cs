using System;
using System.Configuration;

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
            try
            {
                //Managers
                var Manager = new ManagerXmlLinq();
                var CategoryManager = new CategoryManager();
                var ProducerManager = new ProducerManager();
                var GoodManager = new GoodManager();

                //DbWorkers
                var GoodDbWorker = new GoodDbWorker();
                var CategoryDbWorker = new CategoryDbWorker();
                var ProducerDbWorker = new ProducerDbWorker();

                //Read from csv
                var Categories = CategoryManager.ReadFromFile(CategoryPath);
                var Producers = ProducerManager.ReadFromFile(ProducerPath);
                var Goods = GoodManager.ReadFromFile(GoodsPath);
                
                //Getting full goods
                var FullGoods = Manager_FullGoods.Full_Goods(Producers, Categories, Goods);

                Manager.WriteToFile(GoodsXmlPath, FullGoods);
                var LinqGoods = Manager.ReadFromFile(GoodsXmlPath);
                Manager_FullGoods.GoodsCounter(LinqGoods);


                var prod = new Producer()
                {
                    Id = 7,
                    Name = "name2",
                    Country = "chinatown"
                };
                //CategoryDbWorker.InsertCategoriesList(Categories);
                //ProducerDbWorker.InsertProducersList(Producers);
                //GoodDbWorker.InsertGood(Goods[0]);
                //GoodDbWorker.DeleteGoodById(1);
                var goodbase=GoodDbWorker.GetAll();
                var categorybase = CategoryDbWorker.GetAll();
                var producersbase =ProducerDbWorker.GetAll();
                foreach(var i in goodbase)
                {
                    Console.WriteLine(i.ToString());
                }
                //ProducerDbWorker.UpdateProducer(1, "NewName", "NewCountry");
                ProducerDbWorker.DeleteProducerById(3);
                // var a =GoodDbWorker.GetById(1);
                // var a=ProducerDbWorker.GetById(1);
                var b = CategoryDbWorker.GetById(2);
                GoodDbWorker.DeleteGoodWithForeignKeys(1);

                //ProducerDbWorker.InsertProducer(prod);
                //CategoryDbWorker.InsertCategoriesList(Categories);
                //ProducerDbWorker.InsertProducersList(Producers);
                //GoodDbWorker.InsertGoodsList(Goods);

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

