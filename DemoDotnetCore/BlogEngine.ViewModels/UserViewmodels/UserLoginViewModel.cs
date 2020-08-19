using System.ComponentModel.DataAnnotations;

namespace BlogEngine.ViewModels.UserViewmodels
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}