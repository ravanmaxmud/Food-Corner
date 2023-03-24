using Microsoft.AspNetCore.Mvc;
using FoodCorner.Areas.Client.ViewCompanents;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("blogPage")]
    public class BlogPageController : Controller
    {
        [HttpGet("index",Name ="client-blog-index")]
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            ViewBag.SearchBy = searchBy;
            ViewBag.Search = search;

            return View();
        }

        [HttpGet("blog-filter", Name = "client-blog-filter")]
        public async Task<IActionResult> Filter(string? searchBy = null,
           string? search = null,
           [FromQuery] int? categoryId = null,  [FromQuery] int? tagId = null)
        {

            return ViewComponent(nameof(BlogPage), new
            {
                searchBy = searchBy,
                search = search,
                categoryId = categoryId,
                tagId = tagId
            });

        }
    }
}
