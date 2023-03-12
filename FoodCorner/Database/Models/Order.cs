using FoodCorner.Database.Models.Common;
using FoodCorner.Database.Models.Enums;

namespace FoodCorner.Database.Models
{
    public class Order : IAuditable
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal SumTotalPrice { get; set; }
        public int UserId { get; set; }
        public  User User{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
