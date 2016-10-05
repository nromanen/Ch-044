using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace FirstTask
{
    class Program
    {
        static void Main(string[] args)
        {


            /*            ShopContext shop = new ShopContext();

                        shop.FillGoodsByXml("Content/RomXML.xml");

                        shop.WriteAllContextToDb();
            

                       shop.FillAllContextByDb();


                        if (shop.Goods != null)
                            foreach (var good in shop.Goods)
                            {
                                Console.WriteLine(good);
                            }
            */
            Category newCategory = new Category(){
                Id = 3,
                Name = "AmazingCategory"
            };
            Producer newProducer = new Producer(){
                Id = 3,
                Name = "HyperProducer",
                Country = "Kenia"
            };
            /*DbBuilderForCategories.InsertCategory(newCategory);
            DbBuilderForProducers.InsertProducer(newProducer);
            for (int i = 0; i < 99; i++)
            {
                DbBuilderForGoods.InsertGood(new Good()
                {
                    Id = 13 + i,
                    Name = "NewSuperGood" + i.ToString(),
                    Category = newCategory,
                    Producer = newProducer,
                    Price = 145
                });
            }*/
            //Good good = shop.Goods[0];
            Good newSuperGood = new Good()
            {
                Id = 112,
                Name = "UltraGood",
                Producer = newProducer,
                Category = newCategory,
                Price = 45
            };

            DbBuilderForCategories.DeleteCategoryNotHard(newCategory, true);
            //DbBuilderForGoods.InsertGood(newSuperGood);
            Console.ReadKey();            
        }
    }
}
