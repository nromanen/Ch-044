using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ByEntity.Model
{
    public class Producer
    {
        public Producer()
        {
            Goods = new HashSet<Good>();
        }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]

        [Required, MaxLength(80)]
        public string Name { get; set; }

        [XmlAttribute("country")]
        public string Country { get; set; }

        public virtual ICollection<Good> Goods { get; set; }

    }
}
