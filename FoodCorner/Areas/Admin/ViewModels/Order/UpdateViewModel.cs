using Microsoft.Build.Framework;
using FoodCorner.Database.Models.Enums;

namespace FoodCorner.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
