using FoodCorner.Areas.Client.ViewCompanents;
using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        //private readonly IUserService _userService;
        public BasketController(DataContext dataContext, IBasketService basketService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
        }

        [HttpGet("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(MiniBasket), productCookiViewModel);
            }
            return ViewComponent(nameof(MiniBasket));
        }

        [HttpGet("basket-delete/{productId}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var productCookieViewModel = new List<BasketCookieViewModel>();

            //if (_userService.IsAuthenticated)
            //{
            //    var basketProduct = await _dataContext.BasketProducts
            //       .Include(b => b.Basket).FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == productId);

            //    if (basketProduct is null)
            //    {
            //        return NotFound();
            //    }
            //    _dataContext.BasketProducts.Remove(basketProduct);
            //}
            //else
            //{

            //}

            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                return NotFound();
            }
            var productCookieValue = HttpContext.Request.Cookies["products"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            productCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);

            productCookieViewModel!.RemoveAll(pcvm => pcvm.Id == productId);
            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));


            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(MiniBasket), productCookieViewModel);
        }
    }
}
