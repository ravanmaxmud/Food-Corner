using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Client.ViewModels.Account
{
    public class EditAddressViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
