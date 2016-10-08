using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public static class ParseManager
    {       
        public static void CountOfXml(params string[] paths)
        {
            int i = 0;
            foreach(var t in paths)
            {
                i++;
                ThreadStart s = (()=>CalculateCountofxml(t));
                Thread thread = new Thread(s);
                thread.Start();
                Console.WriteLine("Current thread-" + i);
                thread.Join();
            }
        }
        public static int CalculateCountofxml(string Path)
        {
            int count = 0;
            if (Directory.Exists(Path))
            {
                count = Directory.GetFiles(Path, @"*.xml",SearchOption.AllDirectories).Length;
                Console.WriteLine("Result-" + count);
                return count;
            }
            else
            {
                Console.WriteLine("There is no .xml files in this folder!");
            }
            return count;
        }
    }
}
