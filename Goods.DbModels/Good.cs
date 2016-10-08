using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int CategoryId { get; set; }
        public int ProducerId { get; set; }

        [ForeignKey("CategoryId")]
        [XmlElement("Category")]
        public virtual Category Category { get; set; }

        [ForeignKey("ProducerId")]
        [XmlElement("Producer")]
        public virtual Producer Producer { get; set; }

        public override string ToString()
        {
            return Id + "/" + Name + "/" + Price + "/" + Category.Name + "/" + Producer.Name + "/" + Producer.Country;
        }
    }
}
