using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SS_lab1
{
    class DirectoryParser
    {
        //private string folderPath;        
        public Dictionary<int, string> dictionaryPaths;
        private int counter;    
        private string Template { get; set; }
        //private List<Thread> threadList;

        public override string ToString()
        {
            return counter.ToString();
        }
        public DirectoryParser(string folderPath, string template)
        {
            Template = template;
            SearchFile(folderPath);
            
        }

        private void Count(object path)
        {
            Regex regex = new Regex(Template);            
            string text = File.ReadAllText(path.ToString());
            counter += regex.Matches(text).Count;         

        }

        public void SearchFile(string folderPath, bool flag = true)
        {
            if (flag) dictionaryPaths = new Dictionary<int, string>();

           
            foreach (var item in Directory.GetFiles(folderPath).Where(x => Path.GetExtension(x) == ".cs"))
            {
                dictionaryPaths.Add(dictionaryPaths.Count(), item);
            }            

            string[] folders = Directory.GetDirectories(folderPath);
            foreach (var item in folders)
            {
                SearchFile(item, false);
            }
        }

        public void Parser()
        {
            int i = 0;
            counter = 0;
            for (; i < dictionaryPaths.Count -4; i+=5)
            {
                var t1 = Task.Factory.StartNew(() => { Count(dictionaryPaths[i]); });
                var t2 = Task.Factory.StartNew(() => { Count(dictionaryPaths[i + 1]); });
                var t3 = Task.Factory.StartNew(() => { Count(dictionaryPaths[i + 2]); });
                var t4 = Task.Factory.StartNew(() => { Count(dictionaryPaths[i + 3]); });
                var t5 = Task.Factory.StartNew(() => { Count(dictionaryPaths[i + 4]); });
                t1.Wait();
                t2.Wait();
                t3.Wait();
                t4.Wait();
                t5.Wait();
            }

            for(; i < dictionaryPaths.Count; i++)
            {
                Count(dictionaryPaths[i]);
            }





        }


    }
}
