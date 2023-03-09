using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("cartPage")]
    public class CartPageController : Controller
    {
        [HttpGet("index", Name = "client-cart-index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
