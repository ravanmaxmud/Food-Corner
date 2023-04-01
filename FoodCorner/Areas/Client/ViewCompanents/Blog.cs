using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using System.Linq;
using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Contracts.File;
using FoodCorner.Extensions;

namespace FoodCorner.Areas.Client.ViewCompanents
{

    [ViewComponent(Name = "Blog")]
    public class Blog : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
               await _dataContext.Blogs.Include(b => b.BlogTags).Include(b => b.BlogCategories).Include(b => b.BlogFiles)
                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content.Truncate(100,"..."), b.CreatedAt,
                b.BlogTags.Select(b => b.Tag).Select(b => new BlogViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogCategories.Select(b => b.Category).Select(b => new BlogViewModel.CategoryViewModeL(b.Title, b.Parent.Title)).ToList(),
                b.BlogFiles.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogFiles.Take(1).FirstOrDefault()!.FileNameInSystem, UploadDirectory.Blogs)
                : String.Empty)).ToListAsync();

            return View(model);
        }
    }
}
