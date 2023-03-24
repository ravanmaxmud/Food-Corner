using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class BlogTag : BaseEntity , IAuditable
    {
        public string Title { get; set; }
        public List<BlogAndBlogTag> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
