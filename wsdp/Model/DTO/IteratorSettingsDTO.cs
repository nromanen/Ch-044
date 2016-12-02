using Common.Enum;
using System.Collections.Generic;

namespace Model.DTO
{
    public class IteratorSettingsDTO
    {
        public string Url { get; set; }
        public List<string> GoodsIteratorXpathes { get; set; }
        public string XPathPageIterator { get; set; }
        public string UrlMask { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public DownloadMethod DownloadMethod { get; set; }
    }
}