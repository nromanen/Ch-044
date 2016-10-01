using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FirstTask
{
    public class Producer
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("country")]
        public string Country { get; set; }

        public Producer()
        {
            Id = 0;
            Name = "";
            Country = "";
        }
        public Producer(int id, string name, string country)
            {
            Id = id;
            Name = name;
            Country = country;
            }

        public override string ToString()
        {
            return Id.ToString() + " " + Name + " " + Country;
        }
    }
}
