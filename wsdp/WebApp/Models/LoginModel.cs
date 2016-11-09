using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LoginModel
    {
        private string _email;
        private string _password;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email
        {
            get { return _email; }
            set { _email = value?.Trim(); }
        }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимальная длина - 6 символа")]
        public string Password
        {
            get { return _password; }
            set { _password = value?.Trim(); }
        }
    }
}