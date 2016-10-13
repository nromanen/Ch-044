using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Goods.DbModels
{
    [Serializable]
    public class Good
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }

        [ForeignKey("Category_Id")]
        [XmlElement("Category")]
        public virtual Category Category { get; set; }

        [XmlElement("Producer")]
        [ForeignKey("Producer_Id")]
        public virtual Producer Producer { get; set; }
        [XmlIgnore]
        public int Producer_Id { get; set; }
        [XmlIgnore]
        public int Category_Id { get; set; }

        public override string ToString()
        {
            return Id + "/" + Name + "/" + Price + "/" + Category + Producer;
        }
    }
}
