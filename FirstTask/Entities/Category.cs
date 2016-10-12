using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace FirstTask
{
    public class Category
    {   [XmlAttribute("id")]
        [Key]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
        public virtual ICollection<Good> Goods { get; set; }

        public Category()
        {
            Id = 0;
            Name = "";
        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Name;
        }
    }
}
