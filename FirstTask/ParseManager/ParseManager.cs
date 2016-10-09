using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FirstTask
{
    class ParseManager
    {
        private int _count = 0;
        private int _countOfAsigments = 0;
        private int _countOfConsoles = 0;
        private readonly int _countOfThreads = 0;
        List<Thread> threadWorkers = new List<Thread>();
        private int currentIndexThread = 0;


        public override string ToString()
        {
            return "Number of assignments: " + _countOfAsigments.ToString() + " Number of Consoles: " + _countOfConsoles.ToString();
        }
        public ParseManager(int n = 5)
        {
            if (n == 0)
                throw new Exception("Count of threads must be greater than 0");
            _countOfThreads = n;
            for (int i = 0; i < n; i++)
            {
                threadWorkers.Add(new Thread(CalculateForOneFile));
            }
        }

        private void ManageThreadWork(string pathForFile)
        {
            threadWorkers[currentIndexThread] = new Thread(CalculateForOneFile);

            
            threadWorkers[currentIndexThread].Start(pathForFile);
            threadWorkers[currentIndexThread].Join();

            currentIndexThread++;
            if (currentIndexThread == _countOfThreads)
                currentIndexThread = 0;
            
        }
        public void CalculateFromDirectory(string path)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = di.GetFiles("*.cs");

            foreach (var file in files)
            {
                ManageThreadWork(file.FullName);
            }


            foreach (var directory in di.GetDirectories())
            {
                CalculateFromDirectory(directory.FullName);
            }
        }

        public int StartCalculation(string path)
        {
            CalculateFromDirectory(path);           
            return _countOfAsigments;
        }


        /*public void CalculateXmlFiles(params string[] pathes)
        {
            foreach (string path in pathes)
            {
                Thread newThread = new Thread(CalculateCountFromOneFolder);
                newThread.Start(path);
                newThread.Join();
            }
        }
        public void CalculateCountFromOneFolder(object path)
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode());
            string pathForParsing = (string)path;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pathForParsing);
            System.IO.FileInfo[] files = di.GetFiles("*.xml*");
            foreach (var directory in di.GetDirectories())
            {
                CalculateXmlFiles(directory.FullName);
            }
            _count += files.Count();
        }
        public int GetCountOfXmlFiles(params string[] pathes)
        {
            this.CalculateXmlFiles(pathes);
            return _count;
        }*/

        public void CalculateForOneFile(object pathObj)
        {

            lock (this)
            {
            string path = (string)pathObj;
            using (StreamReader sr = new StreamReader(path) )
            {
                string text = sr.ReadToEnd();
                char[] arr = text.ToCharArray();

                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i]=='=' && arr[i-1]!='='&& arr[i-1]!='=' && arr[i+1]!='=' && arr[i-1]!='!' && arr[i-1]!='>' && arr[i-1]!='<')
                    {
                        _countOfAsigments++;
                        i++;
                    }
                }

                int count = Regex.Matches(text, "Console").Count;
                _countOfConsoles += count;
            }
            }
        }
    }
}
