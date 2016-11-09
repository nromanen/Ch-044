using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginModel
    {

        private string email;
        [Required]
        public string Email
        {
            get { return email; }
            set { email = value.Trim(); }
        }

        private string password;
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return password; }
            set { password = value.Trim(); }
        }
    }
}