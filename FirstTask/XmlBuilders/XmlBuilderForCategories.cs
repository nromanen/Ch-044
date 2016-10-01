using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FirstTask
{
    class XmlBuilderForCategories
    {
        static public void WriteToXml(List<Category> list, string path)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(list.GetType());
            x.Serialize(new FileStream(path, FileMode.Create), list);
        }

        static public List<Category> ReadFromXml(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Category>));
            List<Category> resultList = (List<Category>)(x.Deserialize(new FileStream(path, FileMode.Open)));
            return resultList;
        }
    }
}
