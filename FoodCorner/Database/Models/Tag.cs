using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class Tag :BaseEntity,IAuditable
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
