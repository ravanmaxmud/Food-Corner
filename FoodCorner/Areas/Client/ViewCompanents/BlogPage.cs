using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using FoodCorner.Extensions;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "BlogPage")]
    public class BlogPage : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? searchBy = null,
           string? search = null,
           [FromQuery] int? categoryId = null, [FromQuery] int? tagId = null)
        {

            var blogQuery = _dataContext.Blogs.AsQueryable();

            if (searchBy == "Title")
            {
                blogQuery = blogQuery.Where(p => p.Title.StartsWith(search) || search == null);
            }
            else if (categoryId is not null || tagId is not null)
            {

                blogQuery = blogQuery.Where(b => categoryId == null || b.BlogCategories!.Any(bc => bc.BlogCategoryId == categoryId))
                    .Where(b => tagId == null || b.BlogTags!.Any(bt=> bt.BlogTagId == tagId));
            }

            var model  =
              await blogQuery.Include(b => b.BlogTags).Include(b => b.BlogCategories).Include(b => b.BlogFiles)
                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content.Truncate(250,"..."), b.CreatedAt,
                b.BlogTags!.Select(b => b.Tag).Select(b => new BlogViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogCategories!.Select(b => b.Category).Select(b => new BlogViewModel.CategoryViewModeL(b.Title, b.Parent.Title)).ToList(),
                b.BlogFiles!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogFiles.Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blogs)
                : String.Empty)).ToListAsync();

            return View(model);
        }
    }
}
