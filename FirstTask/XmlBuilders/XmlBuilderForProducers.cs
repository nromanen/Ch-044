using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FirstTask
{
    static class XmlBuilderForProducers
    {
        static public void WriteToXml(List<Producer> list, string path)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(list.GetType());
            x.Serialize(new FileStream(path, FileMode.Create), list);
        }

        static public List<Producer> ReadFromXml(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Producer>));
            List<Producer> resultList = (List<Producer>)(x.Deserialize(new FileStream(path, FileMode.Open)));
            return resultList;
        }
    }
}


