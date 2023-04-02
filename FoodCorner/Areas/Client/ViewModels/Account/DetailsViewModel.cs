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
      
    }
}
