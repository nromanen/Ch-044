using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace App1
{
    public class ManagerXmlLinq : IOManager<List<Good>>
    {
        public List<Good> ReadFromFile(string path)
        {
            var MyReader = new System.Configuration.AppSettingsReader();
            string typeofparse = MyReader.GetValue("TypeRead", typeof(string)).ToString();
            if (typeofparse == "linq")
                return LinqBuilder.ReadWithLinq(path);
            else
                return XmlBuilder.ReadWithSerializer(path);
        }
        public void WriteToFile(string path, List<Good> list)
        {
            var MyReader = new System.Configuration.AppSettingsReader();
            string keyvalue = MyReader.GetValue("TypeWrite", typeof(string)).ToString();
            if (keyvalue == "linq")
                LinqBuilder.WriteToXmlLinq(list, path);
            else
                XmlBuilder.WriteToXml(list, path);
        }




    }
}

