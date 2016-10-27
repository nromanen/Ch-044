using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB
{
    public class WebShop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Name { get; set; }

        [Required]
        [Url]
        public string Path { get; set; }
        
        public string LogoPath { get; set; }
    }
}
