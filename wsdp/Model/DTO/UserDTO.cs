using System.ComponentModel.DataAnnotations;
using Resources;

namespace Model.DTO
{
    public class UserDTO
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

        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationPassword")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordConfirmation")]
        public string ConfirmPassword { get; set; }
    }
}