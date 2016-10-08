using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Goods.DbModels;

namespace Goods.Managers
{
    public static class XmlBuilder
    {
        public static void WriteToXml(List<Good> list, string path)
        {
            var formatter = new XmlSerializer(typeof(List<Good>));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, list);

            }
        }
        public static List<Good> ReadWithSerializer(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Good>));
            using (var stream = new StreamReader(path))
            {
                return (List<Good>)serializer.Deserialize(stream);

            }
        }
    }
}
