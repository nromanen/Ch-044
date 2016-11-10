using System.ComponentModel.DataAnnotations;

namespace Model.DB
{
    public class WebShop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Path { get; set; }

        public string LogoPath { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}