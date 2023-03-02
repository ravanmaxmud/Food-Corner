using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Database.Models;

namespace FoodCorner.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
