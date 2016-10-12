using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    partial class ShopContextEf
    {
        public Good GetGoodById(int id)
        {
            try
            {
                return this.Goods.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertGood(Good good)
        {
            try
            {
                Goods.Add(good);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateGood(Good good)
        {
            try
            {
                Good foundGood = this.GetGoodById(good.Id);
                foundGood = good;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RemoveGood(Good good)
        {
            try
            {
                Good foundGood = this.GetGoodById(good.Id);
                this.Goods.Remove(foundGood);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public Producer GetProducerById(int id)
        {
            try
            {
                return this.Producers.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertProducer(Producer producer)
        {
            try
            {
                Producers.Add(producer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateProducer(Producer producer)
        {
            try
            {
                Producer foundProducer = this.GetProducerById(producer.Id);
                foundProducer = producer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void RemoveProducer(Producer producer)
        {
            try
            {
                Producer foundProducer = this.GetProducerById(producer.Id);
                Producers.Remove(foundProducer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Producer GetProducerByNameAndCountry(string name, string country)
        {
            try
            {
                Producer producer = this.Producers.Where(c => (c.Name == name && c.Country == country)).Select(c => c).First();
                return producer;
            }
            catch
            {
                return null;
            }
        }
        public Category GetCategoryById(int id)
        {
            try
            {
                return this.Categories.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertCategory(Category category)
        {
            try
            {
                Categories.Add(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateCategory(Category category)
        {
            try
            {
                Category foundCategory = this.GetCategoryById(category.Id);
                foundCategory = category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void RemoveCategory(Category category)
        {
            try
            {
                Category foundCategory = this.GetCategoryById(category.Id);
                Categories.Remove(foundCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Category GetCategoryByName(string name)
        {
            try
            {
                Category category = this.Categories.Where(c => c.Name == name).Select(c => c).First();
                return category;
            }
            catch
            {
                return null;
            }
        }



        public decimal GetAvaragePriceForOneProducer(Producer producer)
        {
            int id = producer.Id;

            try
            {
                return this.Producers.Find(id).Goods.Select(g => g.Price).Average();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        public void FillByXml(string path)
        {
            List<Good> goods = XmlBuilderForGoods.ReadFromXmlLinq(path);

            foreach (var good in goods)
            {
                var producer = this.GetProducerByNameAndCountry(good.Producer.Name, good.Producer.Country);
                if (producer != null)
                    good.Producer = producer;

                var category = this.GetCategoryByName(good.Category.Name);
                if (category != null)
                    good.Category = category;
                
                this.InsertGood(good);
                this.SaveChanges();
            }
        }

        public void RemoveAll()
        {
            foreach (var category in Categories)
            {
                this.RemoveCategory(category);
            }

            foreach (var producer in Producers)
            {
                this.RemoveProducer(producer);
            }
        }
    }
}
