using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Client.ViewModels.Account
{
    public class DetailsViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
       
        [Required]
        public string Password { get; set; }

        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}", ErrorMessage = "Your password must be at least 5 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password and confirm password is not same")]
        public string ConfirimNewPassword { get; set; }
    }
}
