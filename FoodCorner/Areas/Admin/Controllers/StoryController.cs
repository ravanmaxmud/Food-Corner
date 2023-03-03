using FoodCorner.Areas.Admin.ViewModels.Story;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/story")]
    public class StoryController : Controller
    {
        private readonly DataContext _dataContext;

        public StoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-story-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Stories.Select(s => new ListItemViewModel(s.Id, s.Content)).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-story-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-story-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var story = new Story
            {
                Content = model.Content
            };

            await _dataContext.Stories.AddAsync(story);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-story-list");
        }

        [HttpPost("delete/{id}", Name = "admin-story-delete")]
        public async Task<IActionResult> Delte(int id)
        {
            var story = await _dataContext.Stories.FirstOrDefaultAsync(s=> s.Id == id); 

            if (story is null)
            {
                return NotFound();
            }

            _dataContext.Stories.Remove(story);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-story-list");
        }
    }
}
