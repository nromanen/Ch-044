using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Goods.DbModels
{
    [Serializable]
    public class Good
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
