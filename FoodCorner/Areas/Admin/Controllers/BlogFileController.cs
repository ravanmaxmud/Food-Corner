using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Admin.ViewModels.BlogFile;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogfile")]
    [Authorize(Roles = "admin")]
    public class BlogFileController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public BlogFileController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("{blogId}/blogimage/list", Name = "admin-blogimage-list")]
        public async Task<IActionResult> List([FromRoute] int blogId)
        {
            var product = await _dataContext.Blogs.Include(p => p.BlogFiles).FirstOrDefaultAsync(p => p.Id == blogId);
            if (product == null) return NotFound();

            var model = new BlogFileViewModel { BlogId = product.Id };

            model.Files = product.BlogFiles.Select(p => new BlogFileViewModel.ListItem
            {
                Id = p.Id,
                FileUrl = _fileService.GetFileUrl(p.FileNameInSystem, UploadDirectory.Blogs),
                CreatedAt = p.CreatedAt,
            }).ToList();

            return View(model);

        }
        #region Add

        [HttpGet("{blogId}/blogimage/add", Name = "admin-blogimage-add")]
        public async Task<IActionResult> Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{blogId}/blogimage/add", Name = "admin-blogimage-add")]
        public async Task<IActionResult> Add([FromRoute] int blogId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == blogId);

            if (product is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.File, UploadDirectory.Blogs);

            var productImage = new BlogFile
            {
                Blog = product,
                FileName = model.File.FileName,
                FileNameInSystem = imageNameInSystem,
            };

            await _dataContext.BlogFiles.AddAsync(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogimage-list", new { blogId = blogId });

        }
        #endregion

        #region Delete
        [HttpPost("{blogId}/blogimage/{blogFileId}/delete", Name = "admin-blogimage-delete")]
        public async Task<IActionResult> Delete([FromRoute] int blogId, [FromRoute] int blogFileId)
        {

            var productImage = await _dataContext.BlogFiles.FirstOrDefaultAsync(p => p.BlogId == blogId && p.Id == blogFileId);

            if (productImage is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(productImage.FileNameInSystem, UploadDirectory.Blogs);

            _dataContext.BlogFiles.Remove(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogimage-list", new { blogId = blogId });

        }
        #endregion

    }
}
