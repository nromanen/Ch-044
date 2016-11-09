using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DB
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public string DefaultValue { get; set; }
        public string Prefix { get; set; }
        public string Sufix { get; set; }

        [ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }

        public int Category_Id { get; set; }
    }
}