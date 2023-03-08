using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.Size
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public int PersonSize { get; set; }
        public int? IncreasePercent { get; set; }
    }
}
