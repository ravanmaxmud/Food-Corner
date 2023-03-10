using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("wishList")]
    public class WishListController : Controller
	{
		[HttpGet("index",Name ="client-wishlist-index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
