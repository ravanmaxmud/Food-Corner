using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Database.Models;

namespace FoodCorner.Services.Abstracts
{
    public interface IWishListService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product, ModalViewModel model);
    }
}
