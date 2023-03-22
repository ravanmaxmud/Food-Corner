using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class Comment : BaseEntity, IAuditable
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
