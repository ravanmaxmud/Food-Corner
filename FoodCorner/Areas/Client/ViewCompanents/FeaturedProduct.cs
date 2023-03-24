using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "FeaturedProduct")]
    public class FeaturedProduct : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public FeaturedProduct(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? slide = null)
        {
            var productsQuery = _dataContext.Products.Include(p => p.ProductCatagories).AsQueryable();
            if (slide == "NewProduct")
            {
                productsQuery = productsQuery.OrderByDescending(p => p.CreatedAt).Take(4);
            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.CreatedAt);
            }


            var model = await productsQuery
                .Take(4).Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price, p.DiscountPercent, p.DiscountPrice, p.CreatedAt,
                 p.ProductImages!.Where(p => p.IsPoster == true).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(p.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Product)
                 : String.Empty)).ToListAsync();

            return View(model);
        }
    }
}
