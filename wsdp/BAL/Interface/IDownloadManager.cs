using System;

namespace BAL.Interface
{
    public interface IDownloadManager
    {
        Guid DownloadFromPath(string url);

        void WriteToFile(string source);

        string ReplaceHrefs(string htmlSource, string url);
    }
}