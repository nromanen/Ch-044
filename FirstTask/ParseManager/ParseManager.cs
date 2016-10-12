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
        
        private int _countOfAsigments = 0;
        private int _countOfWords = 0;
        private readonly int _countOfThreads = 0;
        List<Thread> threadWorkers = new List<Thread>();
        private int currentIndexThread = 0;
        private bool _searchAssignments = true;

        public override string ToString()
        {
            return "Number of assignments: " + _countOfAsigments.ToString() + " Number of Consoles: " + _countOfWords.ToString();
        }
        public ParseManager(int n, bool search)
        {
            if (n == 0)
                throw new Exception("Count of threads must be greater than 0");
            _countOfThreads = n;
            for (int i = 0; i < n; i++)
            {
                threadWorkers.Add(new Thread(CalculateForOneFileCountOfWords));
            }

            _searchAssignments = search;
        }

        private void ManageThreadWork(string pathForFile, string word)
        {
            threadWorkers[currentIndexThread] = new Thread(CalculateForOneFileCountOfWords);


            threadWorkers[currentIndexThread].Start(new ArgumentsForParseManager()
                {
                    Word = word,
                    Path = pathForFile
                });
            threadWorkers[currentIndexThread].Join();

            currentIndexThread++;
            if (currentIndexThread == _countOfThreads)
                currentIndexThread = 0;

            if (_searchAssignments)
            {
                threadWorkers[currentIndexThread] = new Thread(CalculateForOneFileCountOfAssignments);


                threadWorkers[currentIndexThread].Start(new ArgumentsForParseManager()
                {
                    Word = null,
                    Path = pathForFile
                });
                threadWorkers[currentIndexThread].Join();

                currentIndexThread++;
                if (currentIndexThread == _countOfThreads)
                    currentIndexThread = 0;
            }

        }

        public void CalculateFromDirectory(string path, string extension, string word)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = di.GetFiles(extension);

            foreach (var file in files)
            {
                ManageThreadWork(file.FullName, word);
            }


            foreach (var directory in di.GetDirectories())
            {
                CalculateFromDirectory(directory.FullName, extension, word);
            }
        }

        public string StartCalculationAndGetResult(string path,string extension, string word)
        {
            CalculateFromDirectory(path,extension, word);
            return "Number of assignments: " + _countOfAsigments.ToString() + " Number of " + word + ": " + _countOfWords.ToString() ;
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

        public void CalculateForOneFileCountOfWords(object pathObj)
        {
            string path = ((ArgumentsForParseManager)pathObj).Path;
            string word = ((ArgumentsForParseManager)pathObj).Word;
            using (StreamReader sr = new StreamReader(path) )
            {
                string text = sr.ReadToEnd();
                int count = Regex.Matches(text,word).Count;
                _countOfWords += count;
            }
        }

        public void CalculateForOneFileCountOfAssignments(object pathObj)
        {
            string path = ((ArgumentsForParseManager)pathObj).Path;
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                char[] arr = text.ToCharArray();

                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] == '=' && arr[i - 1] != '=' && arr[i - 1] != '=' && arr[i + 1] != '=' && arr[i - 1] != '!' && arr[i - 1] != '>' && arr[i - 1] != '<')
                    {
                        _countOfAsigments++;
                        i++;
                    }
                }
            }
        }
    }
}
