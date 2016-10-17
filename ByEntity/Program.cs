using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.Repositories;
using ByEntity.DbServices;


namespace ByEntity
{
    
    class Program
    {
        static void Main(string[] args)
        {
            GoodContext db = new GoodContext();

            var manager = new GoodsLinqToXmlManager();
            List<Good> goods = new List<Good>();
            manager.GoodsFromXml("EntityXml.xml", out goods);
            //List<Category> categories = goods.Select(x => new Category { Id = x.Category.Id, Name = x.Category.Name }).ToList();
            //categories = categories.GroupBy(a => a.Name).Select(x => x.First()).ToList();


            //List<Producer> producers = goods.Select(x => x.Producer).ToList();  

            var service = new GoodServices(db);
            Console.WriteLine(service.AddList(goods));

           


            Console.ReadLine();
              

        }
    }
}
