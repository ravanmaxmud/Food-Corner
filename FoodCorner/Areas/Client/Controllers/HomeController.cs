using FoodCorner.Areas.Client.ViewCompanents;
using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
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
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dataContext.Sliders
                .Select(s => new SliderViewModel(s.Id, s.HeaderTitle, s.MainTitle, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundİmageInFileSystem, UploadDirectory.Slider))).ToListAsync(),

                Categories = await _dataContext.Categories
                .OrderByDescending(c=> c.Id).Take(3).Select(c => new CategoryViewModel(c.Id,c.Title,
                _fileService.GetFileUrl(c.BackgroundİmageInFileSystem,UploadDirectory.Category))).ToListAsync(),

                Stories = await _dataContext.Stories.Take(1).Select(S=> new StoryViewModel(S.Content)).ToListAsync()

                
            };

           

            return View(model);
        }



        [HttpGet("GetModel/{id}", Name = "Product-GetModel")]
        public async Task<ActionResult> GetModelAsync(int id)
        {
            var product = await _dataContext.Products.Include(p=> p.ProductCatagories).Include(p=> p.ProductTags).Include(p => p.ProductImages)
                .Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);

            //var basket = await _dataContext.BasketProducts.FirstOrDefaultAsync(p => p.ProductId == id);


            if (product is null)
            {
                return NotFound();
            }

            var model = new ModalViewModel(product.Id, product.Name, product.Description, product.Price,product.DiscountPrice,
               product.ProductImages
                .Select(p => new ModalViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

                _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ModalViewModel.SizeViewModeL(ps.Size.PersonSize, ps.Size.Id)).ToList(),
                 1,
                 _dataContext.ProductCatagories.Include(ps => ps.Catagory).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new CategoryViewModel(ps.Catagory.Id, ps.Catagory.Title)).ToList(),

                  _dataContext.ProductTags.Include(ps => ps.Tag).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ModalViewModel.TagViewModeL(ps.Tag.Title, ps.Tag.Id)).ToList()
                );
            return PartialView("~/Areas/Client/Views/Shared/_ProductModalPartial.cshtml", model);
        }
    }
}
