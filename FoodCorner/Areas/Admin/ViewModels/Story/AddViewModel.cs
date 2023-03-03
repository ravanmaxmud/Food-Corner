using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.Story
{
    public class AddViewModel
    {
        [Required]
        public string Content { get; set; }
    }
}
