using Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Model.DB
{
    public class Good
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public GoodCategory Category { get; set; }

        [Required]
        public string XmlData { get; set; }
    }
}