using FoodCorner.Areas.Admin.ViewModels.Category;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Migrations;
using FoodCorner.Services.Abstracts;
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
        private readonly IFileService _fileService;


        public CategoryController(DataContext dataContext, ILogger<CategoryController> logger, IFileService fileService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _fileService = fileService;
        }

        [HttpGet("list",Name ="admin-cate-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Categories.Select(c => new ListItemViewModel(c.Id, c.Title,c.Parent.Title, 
                _fileService.GetFileUrl(c.BackgroundİmageInFileSystem, UploadDirectory.Category)))
                .ToListAsync();
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

            var imageNameInSystem = await _fileService.UploadAsync(model.Backgroundİmage, UploadDirectory.Category);

            var category = new Category
            {
                Title = model.Title,
                ParentId = model.CategoryIds,
                Backgroundİmage = model.Backgroundİmage.FileName,
                BackgroundİmageInFileSystem = imageNameInSystem
                
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

            var allCate = await _dataContext.Categories.ToListAsync();

            if (category is null)
            {
                return NotFound();
            }

            foreach (var item in allCate)
            {
                if (category.Id == item.ParentId)
                {
                    await _fileService.DeleteAsync(item.BackgroundİmageInFileSystem, UploadDirectory.Category);
                    _dataContext.Categories.Remove(item);
                    await _dataContext.SaveChangesAsync();
                }
            }

            await _fileService.DeleteAsync(category.BackgroundİmageInFileSystem, UploadDirectory.Category);
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-cate-list");
        }
    }
}
