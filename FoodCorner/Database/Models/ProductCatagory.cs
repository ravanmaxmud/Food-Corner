using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class ProductCatagory : BaseEntity, IAuditable
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CatagoryId { get; set; }
        public Category Catagory { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
