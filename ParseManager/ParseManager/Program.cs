using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseManager pm = new ParseManager();

            Console.WriteLine(pm.GetConcreteGoodsFromOneCategory(@"http://www.moyo.ua/tovary_dlya_doma/tovary_dla/syshki/"));

            Console.Read();
        }
    }
}
