using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    partial class ShopContext
    {
        public void FillGoodsByCsv(string path)
        {
            try
            {
                Goods = FactoryOfBuilders.CreateGoodsBuilder().ReadFromCsv(path);
                this.MakeMappingCsv();
            }
            catch (Exception ex)
            {
                Goods = null;
                Console.WriteLine(ex.Message);
            }

        }
        public void FillCategoriesByCsv(string path)
        {
            try
            {
                //Categories = new CategoryBuilder().ReadFromCsv(path);
                Categories = FactoryOfBuilders.CreateCategoriesBuilder().ReadFromCsv(path);
            }
            catch (Exception ex)
            {
                Categories = null;
                Console.WriteLine(ex.Message);
            }
        }

        public void FillProducersByCsv(string path)
        {
            try
            {
                //Producers = new ProducersBuilder().ReadFromCsv(path);
                Producers = FactoryOfBuilders.CreateProducersBuilder().ReadFromCsv(path);
            }
            catch (Exception ex)
            {
                Producers = null;
                Console.WriteLine(ex.Message);
            }

        }
        public void MakeMappingCsv()
        {
            foreach (var good in Goods)
            {
                int categoryId = good.Category.Id;
                int producerId = good.Producer.Id;
                try
                {
                    good.Category = (from category in Categories
                                     where category.Id == categoryId
                                     select category).First();
                }
                catch
                {
                    Categories.Add(new Category(categoryId, "Noname Category"));
                    good.Category = Categories.Last();
                }


                /*if (good.Category == null)
                {
                    
                }*/

                try
                {
                    good.Producer = (from producer in Producers
                                     where producer.Id == producerId
                                     select producer).First();
                }
                catch
                {
                    Producers.Add(new Producer(producerId, "Noname Producer", "Noname country"));
                    good.Producer = Producers.Last();
                }

                /*if (good.Producer == null)
                {
                    
                }*/

            }
        }

        public void FillProducersByXml(string path)
        {
            Producers = FactoryOfBuilders.CreateProducersBuilder().ReadFromXml(path);
        }

        public void FillCategoriesByXml(string path)
        {
            Categories = FactoryOfBuilders.CreateCategoriesBuilder().ReadFromXml(path);
        }

        public void FillGoodsByXml(string path)
        {
            Goods = FactoryOfBuilders.CreateGoodsBuilder().ReadFromXml(path);
            this.MakeMappingXml();
        }

        public void FillAllContextByDb()
        {
            //DbBuilderForGoods.DropGoodsDb(connString);
            Producers = DbBuilderForProducers.ReadFromDb();
            Categories = DbBuilderForCategories.ReadFromDb();
            Goods = DbBuilderForGoods.ReadFromDb();

            this.MakeMappingCsv();
        }
        public void WriteAllContextToDb()
        {
            DbBuilderForGoods.DropGoodsDb();
            DbBuilderForProducers.FillToDb(Producers);
            DbBuilderForCategories.FillToDb(Categories);
            DbBuilderForGoods.FillToDb(Goods);
        }

        public void MakeMappingXml()
        {
            Category category = null;
            Producer producer = null;
            foreach (var good in Goods)
            {
                try
                {
                    category = (from c in Categories
                                where good.Category.Id == c.Id
                                select c).First();
                    good.Category = category;
                }
                catch
                {
                    Categories.Add(new Category() { Id = good.Category.Id, Name = good.Category.Name });
                    good.Category = Categories.Last();
                }

                try
                {
                    producer = (from c in Producers
                                where good.Producer.Id == c.Id
                                select c).First();
                    good.Producer = producer;
                }
                catch
                {
                    Producers.Add(good.Producer);
                    good.Producer = Producers.Last();
                }
            }
        }

        public void MakeMappingForOneGood(Good good)
        {
            int categoryId = good.Category.Id;
            int producerId = good.Producer.Id;
            try
            {
                good.Category = (from category in Categories
                                 where category.Id == categoryId
                                 select category).First();
            }
            catch
            {
                Categories.Add(new Category(categoryId, "Noname Category"));
                good.Category = Categories.Last();
            }


            /*if (good.Category == null)
            {
                    
            }*/

            try
            {
                good.Producer = (from producer in Producers
                                 where producer.Id == producerId
                                 select producer).First();
            }
            catch
            {
                Producers.Add(new Producer(producerId, "Noname Producer", "Noname country"));
                good.Producer = Producers.Last();
            }
        }
    }
}
