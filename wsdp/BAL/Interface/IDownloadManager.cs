using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IDownloadManager
    {
        Guid DownloadFromPath(string url);
        void WriteToFile(string source);
        string ReplaceHrefs(string htmlSource,string url);
    }
}
