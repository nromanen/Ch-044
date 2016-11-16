using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;


namespace Model.DB
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

        public string Network { get; set; }

        public string NetworkAccountId { get; set; }
    }
}
