using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App1
{
    [Serializable]
    public class Good
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("Category")]
        public Category Category { get; set; }
        [XmlElement("Producer")]
        public Producer Producer { get; set; }
        public override string ToString()
        {
            return Id + "/" + Name + "/" + Price + "/" + Category.Name + "/" + Producer.Name + "/" + Producer.Country;
        }
    }
}
