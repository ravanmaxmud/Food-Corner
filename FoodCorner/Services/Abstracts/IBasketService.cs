using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product,ModalViewModel model);

        Task DeleteBasket([FromRoute] int productId, [FromRoute] int sizeId);
    }
}
