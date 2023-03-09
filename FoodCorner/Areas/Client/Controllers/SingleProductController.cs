using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("singleProduct")]
    public class SingleProductController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SingleProductController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _provider = provider;
        }

        [HttpGet("index",Name ="client-single-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages)
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

            foreach (var item in model.Categories)
            {
                model.Products = await _dataContext.ProductCatagories.Include(p => p.Product)
                .Where(p => p.ProductId != product.Id).Where(p => p.CatagoryId == item.Id)
                .Select(p => new ProductListItemViewModel
                (p.ProductId, p.Product.Name, p.Product.Description, p.Product.Price, p.Product.DiscountPercent, p.Product.DiscountPrice, p.Product.CreatedAt,
                  p.Product.ProductImages!.Where(p => p.IsPoster == true).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(p.Product.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Product)
                 : String.Empty)).ToListAsync();
            }
             
            return View(model);
        }
    }
}
