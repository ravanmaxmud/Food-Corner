using FoodCorner.Areas.Client.ViewCompanents;
using FoodCorner.Areas.Client.ViewModels.Shop;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
	[Area("client")]
	[Route("shop")]
	public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ShopController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("index",Name ="client-shop-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Categories = await _dataContext.Categories.Select(c=> new CategoryViewModel(c.Id,c.Title)).ToListAsync(),

                Tags = await _dataContext.Tags.Select(t=> new TagViewModel(t.Id,t.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpGet("sort",Name ="client-shop-sort")]
		public async Task<IActionResult> Sort([FromQuery] int? sort=null , [FromQuery] int? categoryId = null,
            int? minPrice = null ,
            int? maxPrice = null,
            [FromQuery] int? tagId = null)
		{
			return ViewComponent(nameof(ShopPageProduct), new { sort=sort , categoryId = categoryId , minPrice = minPrice , maxPrice = maxPrice , tagId = tagId });
		}
	}
}
