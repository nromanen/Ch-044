using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class Manager
    {
        List<Thread> threadsList = new List<Thread>();

        public void ThreadsWorker(string path)
        {
            var currentThreadIndex = 0;
            var files = GetFiles(path);
            foreach (var item in files)
            {
                threadsList[currentThreadIndex].Start();
                threadsList[currentThreadIndex].Join();

                currentThreadIndex++;
            }

        }
        public void GetThreadList(string[] pathes)
        {
            foreach (var path in pathes)
            {
                threadsList.Add(new Thread(() => GetCounts(path)));
            }
        }
        public string[] GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            return files;
        }
        public List<int> GetCounts(string filePath)
        {
            var consoleCount = 0;
            var assignmentCount = 0;
            var counts = new List<int>();

            var file = File.ReadAllLines(filePath);
            foreach (var line in file)
            {
                consoleCount += Regex.Matches(line, "Console").Count;
                assignmentCount += Regex.Matches(line, "=").Count;
            }
            counts.Add(consoleCount);
            counts.Add(assignmentCount);


            Console.WriteLine("{0} {1}", consoleCount, assignmentCount);
            return counts;
        }
    }
}
