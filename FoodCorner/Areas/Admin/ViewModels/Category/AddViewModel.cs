using FoodCorner.Areas.Admin.ViewModels.Product;
using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.Category
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile? Backgroundİmage { get; set; }
        public string? BackgroundİmageUrl { get; set; }

        public int? CategoryIds { get; set; }
        public List<CatagoryListItemViewModel>? Catagories { get; set; }
    }
}
