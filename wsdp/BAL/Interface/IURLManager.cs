using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IURLManager
    {
        List<string> GetAllUrls(IteratorSettingsDTO model);
        List<string> GetUrlsFromOnePage(string url, string xpath);
    }
}
