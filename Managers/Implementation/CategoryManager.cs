using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace Goods.Managers
{
    class CategoryManager : IOManager<List<Category>>
    {
        public List<Category> ReadFromFile(string path)
        {
            List<Category> Categories = new List<Category>();
            var lines = File.ReadAllLines(path);
            int Counter = 0;
            foreach (var item in lines.Skip(1))
            {
                Category category = new Category();
                Counter++;
                if ((item.Count(x => x == ';')) == 1)
                {
                    string[] items = item.Split(new char[] { ';' });
                    int _Id;
                    if (int.TryParse(items[0], out _Id) == true)
                    {
                        if (!(Categories.Select(i => i.Id).Contains(_Id)))
                        {
                            category.Id = _Id;
                            string name = items[1];
                            if (!(String.IsNullOrWhiteSpace(name)) && char.IsUpper(name[0]))
                            {
                                category.Name = name;
                                if (category.Id != 0)
                                    Categories.Add(category);
                            }
                            else
                            {
                                Console.WriteLine("Error-Categories:Name is empty or first letter is lowercase on line:{0}", Counter);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error-Categories: with value Id on a line {0}", Counter);
                    }
                }
                else
                {
                    Console.WriteLine("Error-Categories: on a {0} line", Counter);

                }
            }
            return Categories;
        }

        public void WriteToFile(string path, List<Category> list)
        {
            using (var sr = new StreamWriter(path))
            {
                sr.WriteLine("Id;Name");
                foreach (var cat in list)
                {
                    sr.WriteLine(cat.Id + ";" + cat.Name);
                }
            }
        }
    }
}
