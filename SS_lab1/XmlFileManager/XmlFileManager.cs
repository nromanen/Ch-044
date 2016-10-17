using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Configuration;

namespace SS_lab1
{
    public class XmlFileManager : IXmlFileManager
    {
        private string Config
        {
            get
            {
                return ConfigurationSettings.AppSettings["ChooseXmlInOut"].ToString();
            }
        }

        public void GoodsFromXml(string path, out List<Good> goods)
        {
            List<Good> res = new List<Good>();
            if (Int32.Parse(Config) == 1)
            {
                GoodsLinqToXmlManager stream = new GoodsLinqToXmlManager();
                stream.GoodsFromXml(path, out res);                
            }
            else if(Int32.Parse(Config) == 2)
            {
                GoodsXmlManager stream = new GoodsXmlManager();
                stream.GoodsFromXml(path, out res);
            }
            goods = res;
        }

        public void GoodsToXML(string path, List<Good> goods)
        {            
            if (Int32.Parse(Config) == 1)
            {
                GoodsLinqToXmlManager stream = new GoodsLinqToXmlManager();
                stream.GoodsToXML(path, goods);
            }
            else if (Int32.Parse(Config) == 2)
            {
                GoodsXmlManager stream = new GoodsXmlManager();
                stream.GoodsToXML(path, goods);
            }            
        }
    }
}
