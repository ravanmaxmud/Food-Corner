using FoodCorner.Areas.Client.ViewCompanents;
using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using FoodCorner.Services.Concretes;
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
        private readonly IBasketService _basketService;

        public CartPageController(DataContext dataContext, IUserService userService = null, IFileService fileService = null, IBasketService basketService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
            _basketService = basketService;
        }

        [HttpGet("index", Name = "client-cart-index")]
        public async Task<IActionResult> Index()
        {

            return View();
        }


        [HttpPost("add/{id}", Name = "client-cart-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id, ModalViewModel model)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductSizes).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product, model);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(CartPage), productCookiViewModel);
            }
            return ViewComponent(nameof(CartPage), product);
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

        [HttpGet("basket-individual-delete/{productId}/{sizeId}", Name = "client-individual-basket-delete")]
        public async Task<IActionResult> DeleteIndividualProduct([FromRoute] int productId, [FromRoute] int sizeId)
        {

            var productCookieViewModel = new List<BasketCookieViewModel>();
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                    .Include(p => p.Basket).FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == productId && bp.SizeId == sizeId);

                if (basketProduct is null)
                {
                    return NotFound();
                }

                if (basketProduct.Quantity > 1)
                {
                    basketProduct.Quantity -= 1;

                }
                else
                {
                    _dataContext.BasketProducts.Remove(basketProduct);
                }
            }
            else
            {
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

                foreach (var cookieItem in productCookieViewModel)
                {
                    if (cookieItem.Quantity > 1)
                    {
                        cookieItem.Quantity -= 1;
                        cookieItem.Total = cookieItem.Quantity * cookieItem.Price;
                    }
                    else
                    {
                        productCookieViewModel.RemoveAll(p => p.Id == productId && p.SizeId == sizeId);
                        break;
                    }
                }
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
