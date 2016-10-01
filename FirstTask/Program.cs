using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopContext shop = new ShopContext();
            Console.WriteLine("Goods:\n");

            shop.FillGoodsByXml("Content/RomXMl.xml");

            FactoryOfBuilders.CreateGoodsBuilder().WriteToCsv(shop.Goods, "Content/Goods2.csv");


            if (shop.Goods != null)
                foreach (var good in shop.Producers)
                {
                    Console.WriteLine(good);
                }
            Console.ReadKey();            
        }
    }
}
