using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    [Authorize(Roles = "admin")]
    public class AdminHomeController : Controller
    {
        [HttpGet("index",Name ="admin-home-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
