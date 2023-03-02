using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class BasketProduct : BaseEntity,IAuditable
    {
        public int BasketId { get; set; }
        public Basket? Basket{ get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
