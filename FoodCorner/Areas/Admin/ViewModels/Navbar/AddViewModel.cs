using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {
        public string Name { get; set; }

        public string? ToURL { get; set; }

        public List<UrlViewModel>? Urls { get; set; }

        public int Order { get; set; }

        [Required]
        public bool IsViewHeader { get; set; }
        [Required]
        public bool IsViewFooter { get; set; }


        public class UrlViewModel
        {
            public UrlViewModel(string? path)
            {
                Path = path;
            }

            public string? Path { get; set; }
        }
    }
}
