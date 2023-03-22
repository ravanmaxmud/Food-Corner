using FoodCorner.Areas.Client.ViewModels.About;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
	[Area("client")]
	[Route("about")]
	public class AboutController : Controller
	{

        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _provider = provider;
        }

        [HttpGet("index",Name ="client-about-index")]
		public async Task<IActionResult> Index()
		{
			var model = new IndexViewModel
			{
				Teams = await _dataContext.TeamMembers.Select(t => new TeamViewModel($"{t.Name} {t.LastName}", t.CreatedAt, t.InistagramUrl, t.LinkEdinUrl, t.FaceBookUrl,
				_fileService.GetFileUrl(t.MemberİmageInFileSystem, Contracts.File.UploadDirectory.TeamMembers))).ToListAsync(),

				Stories = await _dataContext.Stories.Take(1).Select(S => new StoryViewModel(S.Content)).ToListAsync()
			};
			return View(model);
		}
	}
}
