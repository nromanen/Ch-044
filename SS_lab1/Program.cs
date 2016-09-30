using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Configuration;

namespace SS_lab1
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "Goods1.xml";
            //List<Good> goods = new List<Good>();

            //XmlFileManager m = new XmlFileManager();
            //m.GoodsFromXml(path, out goods);

            //m.GoodsToXML("newGoogs.xml", goods);    

            List<Sale> sales = QueryFromXml.Sales(path);
            sales.Distinct();
            foreach (var item in sales)
            {
                Console.WriteLine(item);
            }
            

            Console.ReadLine();

        }

        
    }
}
