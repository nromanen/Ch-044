using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multithreading
{
    public class ParseManager
    {

        public void ManageThreadWork(List<string> pathes, int numthreads, string symbol)
        {
            object locksum = new object();
            int sum = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            Action<string> processFile = (string value) =>
            {
                Console.WriteLine(value);
                lock (locksum)
                {
                    sum = sum + CalculateOne(value, symbol);
                }
            };

            pathes.AsParallel()
           .WithDegreeOfParallelism(numthreads)
             .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
              .ForAll(processFile);
            stopwatch.Stop();
            Console.WriteLine("Result:" + sum);
            Console.WriteLine("Time:" + stopwatch.ElapsedMilliseconds);
        }

        public List<string> GetPathes(string Path, string format)
        {
            List<string> PathList = new List<string>();
            if (Directory.Exists(Path))
            {
                PathList = Directory.GetFiles(Path, format, SearchOption.AllDirectories).ToList();
                return PathList;
            }
            else
            {
                Console.WriteLine("Folder doesn't exist!");
                return PathList;
            }
        }

        public int CalculateOne(string Path, string symbol)
        {
            lock (this)
            {
                int result = 0;
                int[] arrofcounts = new int[2];
                var lines = File.ReadAllLines(Path);
                foreach (var t in lines)
                {
                    result = result + Regex.Matches(t, symbol).Count;
                }
                return result;
            }
        }
    }
}
