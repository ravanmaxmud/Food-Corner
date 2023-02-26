using FoodCorner.Areas.Admin.Validations;

namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile? PosterImage { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile>? AllImages { get; set; }

        public List<Images>? ImagesUrl { get; set; }
        public List<PosterImages>? PosterImgUrls { get; set; }

        public List<int>? ProductImgIds { get; set; }


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
