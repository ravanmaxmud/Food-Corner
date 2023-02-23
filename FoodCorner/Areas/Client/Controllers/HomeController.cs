using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _provider = provider;
        }


        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("home-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dataContext.Sliders.Select(s => new SliderViewModel(s.Id, s.HeaderTitle, s.MainTitle, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundİmageInFileSystem, UploadDirectory.Slider))).ToListAsync(),
            };

            return View(model);
        }
    }
}
