using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Client.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
