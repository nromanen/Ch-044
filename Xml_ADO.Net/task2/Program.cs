using System;
using System.Collections.Generic;
using task2.Implementations;
using task2.Implementations.LinqReadWrite;
using task2.Implementations.LinqReadWriteXml;
using task2.DBWorkers;

namespace task2
{
	class Program
	{
		static void Main(string[] args)
		{
			string categoryPath = @"D:\softserve\task2\files\categories.csv";
			string producerPath = @"D:\softserve\task2\files\producers.csv";
			string goodPath = @"D:\softserve\task2\files\goods.csv";

			string pathToGoodXml = @"d:\softserve\task2\files\goodsXml.xml";

			try
			{
				var catReader = new Csv_CategoryReader();
				var prodReader = new Csv_ProducerReader();
				var goodReader = new Csv_GoodReader();
				var mngr = new Manager();

				var categories = catReader.ReadFromFile(categoryPath);
				var producers = prodReader.ReadFromFile(producerPath);
				var goods = goodReader.ReadFromFile(goodPath);
				
				//full Goods object
				List<Good> fullGoods = mngr.GoodsBuilder(goods, producers, categories);
                //mngr.PrintGoods(fullGoods);

                //mngr.WriteToFile(pathToGoodXml, fullGoods);
                //var goodsXml = mngr.ReadFromFile(pathToGoodXml);
                //mngr.PrintGoods(goodsXml);

                var producerDbWorker = new ProducerDbWorker();
                // producerDbWorker.CreateProducersList(producers);

                var categoriesDbWorker = new CategoryDbWorker();
                // categoriesDbWorker.InsertCategoriesList(categories);

                var goodsDbWorker = new GoodDbWorker();
                // goodsDbWorker.InsertGoodsList(fullGoods);

                var good = goodsDbWorker.GetById(2);
               Console.WriteLine(good);
                

				

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.ReadLine();
		}
	}
}
