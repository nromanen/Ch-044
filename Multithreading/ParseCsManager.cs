using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public class ParseCsManager
    {

        public void ManageThreadWork(List<string> pathes)
        {
            Action<string> processFile = (string value) =>
            {
                Console.WriteLine(value);
                CalculateOne(value);
                Thread.Sleep(4000);
            };

            pathes.AsParallel()
           .WithDegreeOfParallelism(2)
             .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
              .ForAll(processFile);
        }

        public List<string> GetPathes(string Path)
        {
            List<string> PathList = new List<string>();
            if (Directory.Exists(Path))
            {
                PathList = Directory.GetFiles(Path, @"*.cs", SearchOption.AllDirectories).ToList();
                return PathList;
            }
            else
            {
                Console.WriteLine("Folder doesnt exist!");
                return PathList;
            }
        }

        public int[] CalculateOne(string Path)
        {
            lock (this)
            {
                int concount = 0;
                int eqcount = 0;
                int[] arrofcounts = new int[2];
                var lines = File.ReadAllLines(Path);
                foreach (var t in lines)
                {
                    concount = concount + Regex.Matches(t, "Console").Count;
                    eqcount = eqcount + Regex.Matches(t, "=").Count;
                }
                Console.WriteLine("Count of 'Console' and '=':{0},{1}", concount, eqcount);
                arrofcounts[0] = concount;
                arrofcounts[1] = eqcount;

                return arrofcounts;
            }
        }
    }
}
