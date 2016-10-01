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
                var Manager = new ManagerXmlLinq();
                var CategoryManager = new CategoryManager();
                var ProducerManager = new ProducerManager();
                var GoodManager = new GoodManager();

                var Categories = CategoryManager.ReadFromFile(CategoryPath);
                var Producers = ProducerManager.ReadFromFile(ProducerPath);
                var Goods = GoodManager.ReadFromFile(GoodsPath);
                
                var FullGoods = Manager_FullGoods.Full_Goods(Producers, Categories, Goods);

                Manager.WriteToFile(GoodsXmlPath, FullGoods);
                var LinqGoods = Manager.ReadFromFile(GoodsXmlPath);
                Manager_FullGoods.GoodsCounter(LinqGoods);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadKey();
        }

    }
}

