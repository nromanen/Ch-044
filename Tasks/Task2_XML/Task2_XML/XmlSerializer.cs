using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task2_XML {
	public class XmlSerializer : IXmlManager { 
		public void Serializer(string fileName, List<Good> goods) {         
			System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Good>));

			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
				formatter.Serialize(fs, goods);
			}
		}
		public List<Good> Deserializer(string fileName) {                   
			System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Good>));

			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
				List<Good> Goods = (List<Good>)formatter.Deserialize(fs);
				return Goods;
			}
		}
	}
}
