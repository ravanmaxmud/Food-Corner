namespace FoodCorner.Areas.Client.ViewModels.Wishlist
{
    public class WishlistCookieViewModel
    {
        public WishlistCookieViewModel(int id, string? title, string? imageUrl, decimal price)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Price = price;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
