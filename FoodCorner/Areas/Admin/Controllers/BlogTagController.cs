using FoodCorner.Areas.Admin.ViewModels.BlogTag;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogTag")]
    [Authorize(Roles = "admin")]
    public class BlogTagController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public BlogTagController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-blogTag-list")]
        public async Task<IActionResult> List()
        {
            var model =
                await _dataContext.BlogTags.Select(t => new ListItemViewModel(t.Id, t.Title)).ToListAsync();
            return View(model);
        }


        [HttpGet("add", Name = "admin-blogTag-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-blogTag-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var tag = new BlogTag
            {
                Title = model.Title
            };

            await _dataContext.BlogTags.AddAsync(tag);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-blogTag-list");
        }


        [HttpPost("delete/{id}", Name = "admin-blogTag-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var tags = await _dataContext.BlogTags.FirstOrDefaultAsync(t => t.Id == id);

            if (tags is null)
            {
                return NotFound();
            }
            _dataContext.BlogTags.Remove(tags);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogTag-list");
        }

    }
}
