using FoodCorner.Areas.Client.ViewCompanents;
using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Wishlist;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using FoodCorner.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoodCorner.Areas.Client.Controllers
{
	[Area("client")]
	[Route("wishList")]
	public class WishListController : Controller
	{

        private readonly DataContext _dataContext;
        private readonly IWishListService _wishListService;
        private readonly IUserService _userService;
        public WishListController(DataContext dataContext, IUserService userService, IWishListService wishListService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _wishListService = wishListService;
        }

        [HttpGet("index", Name = "client-wishlist-index")]
		public IActionResult Index(List<WishlistCookieViewModel>? viewModels = null)
		{

            var productsCookieValue = HttpContext.Request.Cookies["wishList"];
            var productsCookieViewModel = new List<WishlistCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<WishlistCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }

		[HttpGet("add", Name = "client-wishlist-add")]
        public async Task<IActionResult> Add(int? id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductSizes).Include(p=> p.ProductImages).FirstOrDefaultAsync(p=> p.Id == id);
            if (product == null) { return NotFound(); }

            var productCookiViewModel = await _wishListService.AddBasketProductAsync(product);

            
            return RedirectToRoute("client-home-index");

        }

        [HttpGet("wishlist-delete/{productId}", Name = "client-wishlist-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var productCookieViewModel = new List<WishlistCookieViewModel>();


            var product = await _dataContext.Products.Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                return NotFound();
            }
            var productCookieValue = HttpContext.Request.Cookies["wishList"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            productCookieViewModel = JsonSerializer.Deserialize<List<WishlistCookieViewModel>>(productCookieValue);

            productCookieViewModel!.RemoveAll(pcvm => pcvm.Id == productId);
            HttpContext.Response.Cookies.Append("wishList", JsonSerializer.Serialize(productCookieViewModel));


            return RedirectToRoute("client-wishlist-index");
        }
    }
}
