using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;

namespace Model.DTO
{
    public class NetworkUserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [MinLength(4, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationUserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationEmail")]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Network { get; set; }

        public string NetworkAccountId { get; set; }
    }
}
