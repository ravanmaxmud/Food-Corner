using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Client.ViewModels.Blog;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "BlogPageCategory")]
    public class BlogPageCategory : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public BlogPageCategory(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.BlogCategories.Select(c => new CategoryListItemViewModel (c.Id, c.Title)).ToListAsync();

            return View(model);
        }
    }
}
