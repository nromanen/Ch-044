using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IDownloadManager
    {
        void DownloadFromPath(string url);
        void WriteToFile(string source, string url);
        string ReplaceHrefs(string htmlSource,string url);
    }
}
