using FoodCorner.Areas.Admin.ViewModels.Product;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ProductController(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService)
        {
            _provider = provider;
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Products
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(p.Name, p.Description, p.Price, p.Id)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await AddProduct();
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            async Task AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                await _dataContext.Products.AddAsync(product);

                if (model.PosterImage is not null)
                {
                    var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Product);


                    var productImage = new ProductImage
                    {
                        Product = product,
                        ImageNames = model.PosterImage.FileName,
                        ImageNameFileSystem = imageNameInSystem,
                        IsPoster = true,

                    };
                    await _dataContext.ProductImages.AddAsync(productImage);
                }
                if (model.AllImages is not null)
                {
                    foreach (var image in model.AllImages!)
                    {
                        var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Product);

                        var productAllImage = new ProductImage
                        {
                            Product = product,
                            ImageNames = image.FileName,
                            ImageNameFileSystem = allImageNameInSystem,
                            IsPoster = false
                        };
                        await _dataContext.ProductImages.AddAsync(productAllImage);
                    }
                }
            }
        }
    }
}
