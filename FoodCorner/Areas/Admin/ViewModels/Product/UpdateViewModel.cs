using FoodCorner.Areas.Admin.Validations;
using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }


        [Required]
        public List<int> CategoryIds { get; set; }
        public List<CatagoryListItemViewModel>? Catagories { get; set; }

        [Required]
        public List<int> TagIds { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }

        [Required]
        public List<int> SizeIds { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }

        public IFormFile? PosterImage { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile>? AllImages { get; set; }
        public List<Images>? ImagesUrl { get; set; }
        public List<PosterImages>? PosterImgUrls { get; set; }
        public List<int>? ProductImgIds { get; set; }

        public int? DiscountPercent { get; set; }
        public int? DiscountPrice { get; set; }


        public class PosterImages
        {
            public PosterImages(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl;
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

        public class Images 
        {
            public Images(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl;
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

    }
}
