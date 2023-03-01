using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Tag
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
