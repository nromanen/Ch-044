using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FirstTask
{
    [XmlRoot(ElementName = "Good")]
    public class Good
    {
        [XmlElement("id")]
        [Key]
        public int Id { get; set; }
        [XmlElement("name")]
    	public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }


        public virtual Category Category{ get; set; }
	    public virtual Producer Producer{ get; set; }

        public Good()
        {
            Id = 0;
            Name = null;
            Price = 0;
            Category = null;
            Producer = null;
        }
        public Good(int id, string name, decimal price, Category category, Producer producer)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            Producer = producer;
        }
        public override string ToString()
        {
            return Id.ToString() + " " + Name + " " + Price.ToString() + " " +
                "\n\t Category: " + Category.ToString() + 
                "\n\t Producer: " + Producer.ToString();
        }

    }
}
