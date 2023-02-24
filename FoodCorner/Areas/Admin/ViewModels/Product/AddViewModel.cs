using FoodCorner.Areas.Admin.Validations;

namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class AddViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile PosterImage { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" , ".jpeg" })]
        public List<IFormFile>? AllImages { get; set; }
    }
}
