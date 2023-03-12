using FoodCorner.Areas.Admin.ViewModels.Tag;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/tag")]
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public TagController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-tag-list")]
        public async Task<IActionResult> List()
        {
            var model = 
                await _dataContext.Tags.Select(t=> new ListItemViewModel(t.Id,t.Title)).ToListAsync();
            return View(model);
        }


        [HttpGet("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var tag = new Tag
            {
                Title = model.Title
            };

            await _dataContext.Tags.AddAsync(tag);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-tag-list");
        }


        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var tags = await _dataContext.Tags.FirstOrDefaultAsync(t=> t.Id == id);

            if (tags is null)
            {
                return NotFound();
            }
             _dataContext.Tags.Remove(tags);

              await _dataContext.SaveChangesAsync();    

            return RedirectToRoute("admin-tag-list");
        }





    }
}
