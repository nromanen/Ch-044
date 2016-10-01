using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    static class XmlBuilderForProducersCount
    {
        static public void WriteToXml(string path, List<ProducersGoodsCount> list)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(list.GetType());
                x.Serialize(fs, list);
            }
        }
    }
}
