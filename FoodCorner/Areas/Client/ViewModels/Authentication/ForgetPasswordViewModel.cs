using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Client.ViewModels.Authentication
{
    public class ForgetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
