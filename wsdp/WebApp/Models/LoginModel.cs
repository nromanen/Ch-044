using System.ComponentModel.DataAnnotations;
using Resources;

namespace WebApp.Models
{
    public class LoginModel
    {
        private string email;
        private string password;

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationEmail")]
        public string Email
        {
            get { return email; }
            set { email = value?.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationPassword")]
        public string Password
        {
            get { return password; }
            set { password = value?.Trim(); }
        }
    }
}