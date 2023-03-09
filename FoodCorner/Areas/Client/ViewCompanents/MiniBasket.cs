using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Migrations;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "MiniBasket")]
    public class MiniBasket : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public MiniBasket(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<BasketCookieViewModel>? viewModels = null)
        {
       
            if (_userService.IsAuthenticated)
            {     

                var model = await _dataContext.BasketProducts.Where(p => p.Basket.UserId == _userService.CurrentUser.Id)
                    .Select(p =>
                    new BasketCookieViewModel(p.Product.Id, p.Product.Name, p.Product.ProductImages.Take(1).FirstOrDefault() != null
                              ? _fileService.GetFileUrl(p.Product.ProductImages.Take(1).FirstOrDefault().ImageNameFileSystem, Contracts.File.UploadDirectory.Product)
                                  : String.Empty,
                                       p.Quantity,
                                       p.SizeId,
                                      _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == p.Product.Id)
                                             .Select(ps => new SizeListItemViewModel(ps.SizeId, ps.Size.PersonSize)).ToList(),
                                         p.SizeId != null
                                         ? _dataContext.Sizes.FirstOrDefault(s => s.Id == p.SizeId).PersonSize
                                         : _dataContext.Sizes.FirstOrDefault().PersonSize,
                                          p.Product.DiscountPrice == null ? (decimal)p.CurrentPrice : (decimal)p.CurrentDiscountPrice,
                                          p.Product.DiscountPrice == null ? (int)p.CurrentPrice * p.Quantity : (int)p.CurrentDiscountPrice * p.Quantity)).ToListAsync();

                return View(model);
            }


            if (viewModels is not null)
            {
                return View(viewModels);
            }

            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<BasketCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
