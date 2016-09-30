using SS_lab1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1
{
    interface IXmlFileManager
    {
        void GoodsToXML(string path, List<Good> goods);
        void GoodsFromXml(string path, out List<Good> goods);

    }
}
