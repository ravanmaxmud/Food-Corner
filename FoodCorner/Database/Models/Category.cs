using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class Category : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<ProductCatagory> ProductCatagories { get; set; }
        public List<Category> Catagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
