using FoodCorner.Database.Models;

namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class FeedBackViewModel
    {
        public FeedBackViewModel(int id, string commentTitle, int productId, string productName, string productImgUrl, string userFullName)
        {
            Id = id;
            CommentTitle = commentTitle;
            ProductId = productId;
            ProductName = productName;
            ProductImgUrl = productImgUrl;
            UserFullName = userFullName;
        }

        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImgUrl { get; set; }
        public string UserFullName { get; set; }
    }
}
