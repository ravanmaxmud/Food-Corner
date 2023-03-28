using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.Address
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
