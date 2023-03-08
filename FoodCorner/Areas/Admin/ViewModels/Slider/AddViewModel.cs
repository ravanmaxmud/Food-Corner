using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        [Required]
        public string HeaderTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public IFormFile? Backgroundİmage { get; set; }
        public string? BackgroundİmageUrl { get; set; }
        [Required]
        public string Button { get; set; }
        [Required]
        public string ButtonRedirectUrl { get; set; }

        public List<UrlViewModel>? Urls { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public class UrlViewModel
        {
            public UrlViewModel(string? routeName, string? path)
            {
                RouteName = routeName;
                Path = path;
            }
            public UrlViewModel()
            {

            }

            public string? RouteName { get; set; }
            public string? Path { get; set; }
        }

    }
}
