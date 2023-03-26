using FoodCorner.Areas.Admin.ViewModels.BlogCategory;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FoodCorner.Areas.Admin.ViewModels.BlogCategory.AddViewModel;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogCategory")]
    [Authorize(Roles = "admin")]
    public class BlogCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;
        private readonly IFileService _fileService;


        public BlogCategoryController(DataContext dataContext, ILogger<CategoryController> logger, IFileService fileService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-blogCate-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.BlogCategories.Select(c => new ListItemViewModel(c.Id, c.Title))
                .ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-blogCate-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Catagories = await _dataContext.BlogCategories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToListAsync(),
            };
            return View(model);
        }
        [HttpPost("add", Name = "admin-blogCate-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            var category = new BlogCategory
            {
                Title = model.Title,
                ParentId = model.CategoryIds,

            };
            await _dataContext.BlogCategories.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blogCate-list");


            IActionResult GetView(AddViewModel model)
            {
                model.Catagories = _dataContext.BlogCategories
                .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                return View(model);
            }
        }


        [HttpPost("delete/{id}", Name = "admin-blogCate-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var category = await _dataContext.BlogCategories.FirstOrDefaultAsync(c => c.Id == id);

            var allCate = await _dataContext.BlogCategories.ToListAsync();

            if (category is null)
            {
                return NotFound();
            }

            foreach (var item in allCate)
            {
                if (category.Id == item.ParentId)
                {
                    _dataContext.BlogCategories.Remove(item);
                    await _dataContext.SaveChangesAsync();
                }
            }

            _dataContext.BlogCategories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blogCate-list");
        }
    }
}
