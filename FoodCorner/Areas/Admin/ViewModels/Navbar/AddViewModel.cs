using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {
        public string Name { get; set; }

        public string ToURL { get; set; }

        public int Order { get; set; }

        [Required]
        public bool IsViewHeader { get; set; }
        [Required]
        public bool IsViewFooter { get; set; }
    }
}
