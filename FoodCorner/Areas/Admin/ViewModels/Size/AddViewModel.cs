using FoodCorner.Areas.Admin.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Size
{
    public class AddViewModel
    {
        [Required]
        public int PersonSize { get; set; }
        [Required]
        public int IncreasePercent { get; set; }
    }
}
