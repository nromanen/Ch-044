using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ExtendedXmlSerialization;
using Model.DTO;

namespace BAL.Manager {
	public class SerializerForGrabber {
		/// <summary>
		/// Deserialize list of Grabber properties, use in MappingProfile
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="type"></param>
		/// <returns>Object of GrabberSettingDTO</returns>
		public object Deserialize(string xml, Type type) {
			var serializer = new XmlSerializer(type);
			using (var stream = new StringReader(xml)) {
				return (GrabberSettingsDTO)serializer.Deserialize(stream);
			}
		}
	}
}
