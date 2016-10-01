using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FirstTask
{
    public class ProducersGoodsCount
    {
        public Producer Producer { get; set; }
        public int CountOfGoods { get; set; }

        static public List<ProducersGoodsCount> GetProducerGoodsCountFromXmlUsingLinq(string path)
        {
            List<ProducersGoodsCount> resultList = new List<ProducersGoodsCount>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string xml = sr.ReadToEnd();
                    XDocument doc = XDocument.Parse(xml);

                    var producers = doc.Descendants("Good").
                        GroupBy(good => good.Element("Producer").Attribute("id").Value,
                        good => new Producer()
                                        {
                                            Id = int.Parse(good.Element("Producer").Attribute("id").Value),
                                            Name = good.Element("Producer").Attribute("name").Value,
                                            Country = good.Element("Producer").Attribute("country").Value
                                        });

                    foreach (var p in producers)
                    {
                        resultList.Add(new ProducersGoodsCount()
                        {
                            Producer = p.First(),
                            CountOfGoods = p.Count()
                        });
                    }




                }
                return resultList;
            } 
        }

        static public decimal GetAveragePriceFromPresetProducer(string path, Producer producer)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string xml = sr.ReadToEnd();
                    XDocument doc = XDocument.Parse(xml);
                    decimal price;

                    try
                    {
                         price = doc.Descendants("Good").Where(good => int.Parse(good.Element("Producer").Attribute("id").Value) == producer.Id)
                        .Select(good => decimal.Parse(good.Element("price").Value))
                        .Average();
                    }
                    catch
                    {
                        Console.WriteLine("Producer Not Found.");
                        return 0;
                    }
                    return price;
                }

            } 
        }

        public override string ToString()
        {
            return Producer.ToString() + " Count: " + CountOfGoods.ToString();
        }

        static public void WriteToXml(string path, List<ProducersGoodsCount> list)
        {
            XmlBuilderForProducersCount.WriteToXml(path, list);
        }
    }
}
