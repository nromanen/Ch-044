using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Goods.DbModels;

namespace Goods.Managers
{
    public class GoodManager : IOManager<List<Good>>
    {
        public List<Good> ReadFromFile(string path)
        {
            List<Good> Goods = new List<Good>();
            var lines = File.ReadAllLines(path);
            int Counter = 0;
            foreach (var item in lines.Skip(1))
            {
                Good good = new Good();
                good.Category = new Category();
                good.Producer = new Producer();
                Counter++;
                if ((item.Count(x => x == ';')) == 4)
                {
                    string[] items = item.Split(new char[] { ';' });
                    int _Id;
                    if (int.TryParse(items[0], out _Id) == true)
                    {
                        if (!(Goods.Select(i => i.Id).Contains(_Id)))
                        {
                            good.Id = _Id;
                            string name = items[1];
                            if (!(String.IsNullOrWhiteSpace(name)) && char.IsUpper(name[0]))
                            {
                                good.Name = name;
                                decimal Price;

                                if ((decimal.TryParse(items[2], out Price) == true) && (ContainsAtMostTwoNumbersAfterComma(items[2]) == true))
                                {
                                    good.Price = Price;

                                    int category_Id;
                                    if (int.TryParse(items[3], out category_Id) == true)
                                    {
                                        good.Category.Id = category_Id;
                                        int producer_Id;

                                        if (int.TryParse(items[4], out producer_Id) == true)
                                        {
                                            good.Producer.Id = producer_Id;
                                            if (good.Id != 0)
                                                Goods.Add(good);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error-Goods: Error with value producer_Id on a line {0}", Counter);
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("Error-Goods: Erorr with value category_Id on a line {0}", Counter);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error-Goods: Erorr with value Price and 2 number after dot on a line {0}", Counter);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error-Goods: Error Name is empty or first letter is lowercase on line:{0}", Counter);
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Error-Goods: with value Id on a line {0}", Counter);
                    }

                }
                else
                {
                    Console.WriteLine("Error-Goods: Splitter error :on a {0} line", Counter);

                }
            }
            return Goods;
        }

        public void WriteToFile(string path, List<Good> list)
        {
            using (var sr = new StreamWriter(path))
            {
                sr.WriteLine("Id;Name;Price;Category_Id;Producer_Id");
                foreach (var good in list)
                {
                    sr.WriteLine(good.Id + ";" + good.Name + ";" + good.Price + ";" + good.Category.Id + ";" + good.Producer.Id);
                }
            }
        }

        bool ContainsAtMostTwoNumbersAfterComma(string str)
        {
            int dotIndex = str.IndexOf(",");
            var res = ((str.Length - 1) - dotIndex);
            return ((str.Length - 1) - dotIndex) <= 2;
        }
    }
}
