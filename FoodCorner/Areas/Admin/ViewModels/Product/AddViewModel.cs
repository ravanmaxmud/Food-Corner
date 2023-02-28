using FoodCorner.Areas.Admin.Validations;
using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; }
        [Required]
        public List<CatagoryListItemViewModel>? Catagories { get; set; }

        [Required]
        public List<int> TagIds { get; set; }
        [Required]
        public List<TagListItemViewModel>? Tags { get; set; }
        [Required]
        public List<int> SizeIds { get; set; }
        [Required]
        public List<SizeListItemViewModel>? Sizes { get; set; }

        public int? DiscountPercent { get; set; }
        public int? DiscountPrice { get; set; }

        [Required]
        public IFormFile PosterImage { get; set; }

        [Required]
        public List<IFormFile>? AllImages { get; set; }
    }
}
