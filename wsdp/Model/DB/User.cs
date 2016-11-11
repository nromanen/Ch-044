using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.DB
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Minimum length - 4 characters")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum length - 6 characters")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

        public string SocialNetwork { get; set; }
    }
}
