using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_XML {
	interface IXmlManager  {
		List<Good> Deserializer(string path);				

		void Serializer(string fileName, List<Good> list);	
	}
}
