using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class WebShopDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Path { get; set; }

        public string LogoPath { get; set; }
    }
}