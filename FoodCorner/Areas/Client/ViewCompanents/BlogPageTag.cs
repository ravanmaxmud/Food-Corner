using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Client.ViewModels.Blog;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "BlogPageTag")]
    public class BlogPageTag : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public BlogPageTag(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync();

            return View(model);
        }
    }
}
