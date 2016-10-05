using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task2.Implementations.LinqReadWriteXml
{
	public class ReadWriteSerializer : IFileManager<List<Good>>
	{
		public List<Good> ReadFromFile(string path)
		{
			var serializer = new XmlSerializer(typeof(List<Good>));
			using (var stream = File.OpenRead(path))
			{
				return (List<Good>)serializer.Deserialize(stream);
			}
		}

		public void WriteToFile(string path, List<Good> Goods)
		{
			var serializer = new XmlSerializer(typeof(List<Good>));
			using (var stream = File.Create(path))
			{
				serializer.Serialize(stream, Goods);
			}
		}
	}
}
