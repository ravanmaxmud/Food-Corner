using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.BlogTag
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
