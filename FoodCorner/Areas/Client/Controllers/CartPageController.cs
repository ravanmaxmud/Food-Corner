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
    [Route("cartPage")]
    public class CartPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CartPageController(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "client-cart-index")]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpGet("cart-delete/{productId}/{sizeId}", Name = "client-cart-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId, [FromRoute] int sizeId)
        {
            var productCookieViewModel = new List<BasketCookieViewModel>();

            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts
                   .Include(b => b.Basket).FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == productId && bp.SizeId == sizeId);

                if (basketProduct is null)
                {
                    return NotFound();
                }
                _dataContext.BasketProducts.Remove(basketProduct);
            }
            else
            {
                var product = await _dataContext.Products.Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == productId);
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

                productCookieViewModel!.RemoveAll(pcvm => pcvm.Id == productId && pcvm.SizeId == sizeId);
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(CartPage), productCookieViewModel);
        }

        [HttpGet("update", Name = "client-cart-update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return ViewComponent(nameof(MiniBasket));
        }
    }
}
