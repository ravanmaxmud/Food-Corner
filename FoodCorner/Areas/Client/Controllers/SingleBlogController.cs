using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
	[Area("client")]
	[Route("singleBlog")]
	public class SingleBlogController : Controller
	{
		private readonly IActionDescriptorCollectionProvider _provider;
		private readonly DataContext _dataContext;
		private readonly IFileService _fileService;
		private readonly IUserService _userService;

		public SingleBlogController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider, IUserService userService)
		{
			_dataContext = dataContext;
			_fileService = fileService;
			_provider = provider;
			_userService = userService;
		}

		[HttpGet("index", Name = "client-singleBlog-index")]
		public async Task<IActionResult> Index(int id)
		{
			var blog = await _dataContext.Blogs.Include(b=> b.BlogFiles).Include(b=> b.BlogCategories).Include(b=>b.BlogTags).FirstOrDefaultAsync(b => b.Id == id);
			if (blog == null) { return NotFound(); }

			var model = new BlogViewModel(blog.Id, blog.Title, blog.Content, blog.CreatedAt,

				  _dataContext.BlogAndBlogTags.Include(ps => ps.Tag).Where(ps => ps.BlogId == blog.Id)
				.Select(ps => new BlogViewModel.TagViewModel(ps.Tag.Title)).ToList(),

				   _dataContext.BlogAndBlogCategories.Include(ps => ps.Category).Where(ps => ps.BlogId == blog.Id)
				.Select(ps => new BlogViewModel.CategoryViewModeL(ps.Category.Title, ps.Category.Parent.Title)).ToList(),

				  blog.BlogFiles!.Take(1).FirstOrDefault() != null
				? _fileService.GetFileUrl(blog.BlogFiles.Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blogs)
				: String.Empty);

			return View(model);
		}
	}
}
