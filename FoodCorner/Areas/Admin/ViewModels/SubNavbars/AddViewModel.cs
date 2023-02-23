using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.SubNavbars
{
    public class AddViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string? ToURL { get; set; }
        public List<UrlViewModel>? Urls { get; set; }


        [Required]
        public int Order { get; set; }

        [Required]
        public int NavbarId { get; set; }
        public List<NavbarListItemViewModel>? Navbar { get; set; }


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
