using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SS_lab1
{
    public class GoodsXmlManager : IXmlFileManager
    {
        public void GoodsFromXml(string path, out List<Good> goods)
        {
            goods = new List<Good>();
            XmlSerializer serializer = new XmlSerializer(goods.GetType());
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fs);
                goods = (List<Good>)serializer.Deserialize(reader);
            }
        }

        public void GoodsToXML(string path, List<Good> goods)
        {
            XmlSerializer format = new XmlSerializer(goods.GetType());
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                format.Serialize(fs, goods);

            }
        }
    }
}
