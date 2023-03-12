using FoodCorner.Areas.Client.ViewModels.Wishlist;
using FoodCorner.Database.Models;

namespace FoodCorner.Services.Abstracts
{
    public interface IWishListService
    {
        Task<List<WishlistCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
