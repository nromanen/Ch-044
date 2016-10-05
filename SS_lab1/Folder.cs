using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1
{
    class Folder
    {
        public string folderPath;
        public double commonXmlSize;
        public void Count(object k)
        {
            
            string[] files = Directory.GetFiles(folderPath);            

            for (int i = 0; i < files.Length; i++)
            {
                commonXmlSize += (Path.GetExtension(files[i]) == ".xml") ? new FileInfo(files[i]).Length : 0;

            }
            Console.WriteLine(k.ToString());
        }
    }
}
