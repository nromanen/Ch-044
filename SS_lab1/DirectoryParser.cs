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
        private Dictionary<int, string> _dictionaryPaths;
        private int _counter;    
        private string Template { get; set; }        

        public override string ToString()
        {
            return _counter.ToString();
        }
        public DirectoryParser(string folderPath, string template)
        {
            Template = template;
            SearchFile(folderPath);
            
        }

        private void Count(object path)
        {
            object locksum = new object();
            Regex regex = new Regex(Template);            
            string text = File.ReadAllText(path.ToString());
            int k = regex.Matches(text).Count;
            lock(locksum){ _counter += k; }       

        }

        public void SearchFile(string folderPath, bool flag = true)
        {
            if (flag) _dictionaryPaths = new Dictionary<int, string>();

           
            foreach (var item in Directory.GetFiles(folderPath).Where(x => Path.GetExtension(x) == ".cs"))
            {
                _dictionaryPaths.Add(_dictionaryPaths.Count(), item);
            }            

            string[] folders = Directory.GetDirectories(folderPath);
            foreach (var item in folders)
            {
                SearchFile(item, false);
            }
        }
        public void Parser()
        {
            _counter = 0;
            for (int i = 0; i < _dictionaryPaths.Count(); i++)
            {
                Count(_dictionaryPaths[i]);
            }
        }

        public void TskParser()
        {
            int i; 
            _counter = 0;
            for (i = 0; _dictionaryPaths.Count() - 1 - i >= 4 ; i+=5)
            {
                var t1 = Task.Factory.StartNew(() => { Count(_dictionaryPaths[i]); });
                var t2 = Task.Factory.StartNew(() => { Count(_dictionaryPaths[i + 1]); });
                var t3 = Task.Factory.StartNew(() => { Count(_dictionaryPaths[i + 2]); });
                var t4 = Task.Factory.StartNew(() => { Count(_dictionaryPaths[i + 3]); });
                var t5 = Task.Factory.StartNew(() => { Count(_dictionaryPaths[i + 4]); });
                t1.Wait();
                t2.Wait();
                t3.Wait();
                t4.Wait();
                t5.Wait();
            }            
         
            while(i < _dictionaryPaths.Count())
            { 
                Count(_dictionaryPaths[i]);
                i++;
            }





        }


    }
}
