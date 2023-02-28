using FoodCorner.Areas.Admin.ViewModels.Category;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Category = FoodCorner.Database.Models.Category;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        [HttpGet("list",Name ="admin-cate-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Categories.Select(c => new ListItemViewModel(c.Id, c.Title,c.Parent.Title)).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-cate-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Catagories = await _dataContext.Categories
                   .Select(c => new ViewModels.Product.CatagoryListItemViewModel(c.Id, c.Title))
                   .ToListAsync(),
            };
            return View(model);
        }
        [HttpPost("add", Name = "admin-cate-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }


            var category = new Category
            {
                Title = model.Title,
                ParentId = model.CategoryIds
            };
            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-cate-list");


            IActionResult GetView(AddViewModel model)
            {
                model.Catagories = _dataContext.Categories
                .Select(c => new ViewModels.Product.CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                return View(model);
            }
        }


        [HttpPost("delete/{id}", Name = "admin-cate-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c=> c.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-cate-list");
        }
    }
}
