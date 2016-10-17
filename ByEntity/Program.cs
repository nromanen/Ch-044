using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByEntity.Model;
using ByEntity.Repositories;
namespace ByEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            GoodContext db = new GoodContext();

            //BaseRepository<Good> goodRep = new BaseRepository<Good>(db);
            //BaseRepository<Category> catRep = new BaseRepository<Category>(db);
            //Producer prod = new Producer() { Id = 2, Name = "B", Country = "B" };
            //Category cat = catRep.Get(2);
            //cat.Name = "Kokoko";
            //db.SaveChanges();
            //Good newGood = new Good() { Id = 1, Name = "Alisa", Price = 120000, Producer = prod, Category = cat };

            //goodRep.Add(newGood);

            //BaseRepository<Producer> pr = new BaseRepository<Producer>(db);

            //Producer fromBase = pr.Get(5);

            //fromBase.Name = "Alisa2";
            //pr.Update(fromBase, 4);

            var categoriesList = db.Categories.ToList();



            //foreach (var item in categoriesList)
            //{
            //    if(item.Goods.Where(x=>x.) != null)
            //    { 
            //        foreach (var i in item.Goods.ToList())
            //        {
            //            Console.WriteLine(i);
            //        }
            //    }
            //}



            
           // Console.WriteLine(db.Categories.Find(a));
            Console.ReadLine();
              

        }
    }
}
