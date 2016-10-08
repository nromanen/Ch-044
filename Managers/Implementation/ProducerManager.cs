using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace Goods.Managers
{
    class ProducerManager : IOManager<List<Producer>>
    {
        public List<Producer> ReadFromFile(string path)
        {
            List<Producer> Producers = new List<Producer>();
            var lines = File.ReadAllLines(path);
            int Counter = 0;
            foreach (var item in lines.Skip(1))
            {
                Producer producer = new Producer();
                Counter++;
                if ((item.Count(x => x == ';')) == 2)
                {
                    string[] items = item.Split(new char[] { ';' });
                    int Id;
                    if (int.TryParse(items[0], out Id) == true)
                    {
                        producer.Id = int.Parse(items[0]);
                        var name = items[1];
                        if (!(String.IsNullOrWhiteSpace(name)) && char.IsUpper(name[0]))
                        {
                            producer.Name = name;
                            var country = items[2];
                            if (!(String.IsNullOrWhiteSpace(items[2])) && char.IsUpper(country[0]))
                            {
                                if (!(country.Where(x => char.IsDigit(x)).Any()))
                                {

                                    producer.Country = country;
                                    if (producer.Id != 0)
                                        Producers.Add(producer);
                                }
                                else
                                {
                                    Console.WriteLine("Error-Producer: Erorr with value Country - consist digit on line :{0}", Counter);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error-Producer: Error with value Country is empty or first letter is lowercase on line:{0}", Counter);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error-Producer: Error with value Name is empty or first letter is lowercase on line:{0}", Counter);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error-Producer: Error with value Id on a line {0}", Counter);
                    }
                }
                else
                {
                    Console.WriteLine("Error-Producer: Splitter error-on a {0} line", Counter);
                }
            }
            return Producers;
        }


        public void WriteToFile(string path, List<Producer> list)
        {
            using (var sr = new StreamWriter(path))
            {
                sr.WriteLine("Id;Name;Country");
                foreach (var prod in list)
                {
                    sr.WriteLine(prod.Id + ";" + prod.Name + ";" + prod.Country);
                }
            }
        }
    }
}
