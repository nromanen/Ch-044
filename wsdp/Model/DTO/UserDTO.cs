using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Минимальная длина - 4 символа")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимальная длина - 6 символа")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }
}