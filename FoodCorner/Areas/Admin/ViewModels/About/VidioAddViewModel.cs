using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.About
{
    public class VidioAddViewModel
    {
        [Required]
        public IFormFile? Vidio { get; set; }
        public string? VidioUrl { get; set; }
    }
}
