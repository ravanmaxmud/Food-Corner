using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class Product : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
