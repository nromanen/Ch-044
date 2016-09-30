using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1_CSV_with_LINQ;

namespace Task1_CSV_with_LINQ
{
    class Program
    {
		
        static void Main(string[] args)
        {									//My version, this wasn`t my task, I wanna try to deserialize CSV files with LINQ
            CsvProducerReader producerReader = new CsvProducerReader();
            var a = producerReader.ReadFromFile("Producer.csv");
            CsvCategoryReader categoryReader = new CsvCategoryReader();
            var b = categoryReader.ReadFromFile("Category.csv");
            CsvGoodReader goodReader = new CsvGoodReader();
            var c = goodReader.ReadFromFile("Goods.csv",a,b);

			Console.ReadKey();
        }
    }
}
