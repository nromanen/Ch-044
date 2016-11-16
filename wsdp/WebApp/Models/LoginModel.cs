using System.ComponentModel.DataAnnotations;
using Resources;

namespace WebApp.Models
{
    public class LoginModel
    {
        private string name;
        private string password;

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Name
        {
            get { return name; }
            set { name = value?.Trim(); }
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