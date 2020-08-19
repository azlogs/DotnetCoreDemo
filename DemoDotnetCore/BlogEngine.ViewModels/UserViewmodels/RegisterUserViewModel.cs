using System.ComponentModel.DataAnnotations;

namespace BlogEngine.ViewModels.UserViewmodels
{
    public class RegisterUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(255, ErrorMessage = "Username Must be between 6 and 20 characters", MinimumLength = 6)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 6 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Fullname { get; set; }
    }
}