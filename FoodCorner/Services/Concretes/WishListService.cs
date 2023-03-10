using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoodCorner.Services.Concretes
{
    public class WishListService : IWishListService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;



        public WishListService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IFileService fileService, IUserService userService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product, ModalViewModel model)
        {
            model = new ModalViewModel
            {
                SizeId = model.SizeId != null ? model.SizeId : _dataContext.Sizes.FirstOrDefault().Id,
                Quantity = model.Quantity != 0 ? model.Quantity : 1,
            };

            var allSize = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == model.SizeId);

            var increasePrice = (product.Price * allSize!.IncreasePercent) / 100;
            var sizePrice = product.Price + increasePrice;

            var increaseDiscountPrice = (product.DiscountPrice * allSize!.IncreasePercent) / 100;
            var sizeDiscountPrice = product.DiscountPrice + increasePrice;


            return await AddCookie();


            async Task<List<BasketCookieViewModel>> AddCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["wishList"];

                var productCookieViewModel = productCookieValue is not null
                   ? JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue)
                   : new List<BasketCookieViewModel> { };

                var cookieViewModel = productCookieViewModel!.FirstOrDefault(pc => pc.Id == product.Id && pc.SizeId == model.SizeId);

                if (cookieViewModel is null || cookieViewModel.SizeId != model.SizeId)
                {

                    productCookieViewModel!.Add
                           (new BasketCookieViewModel(product.Id, product.Name, product.ProductImages!.Take(1).FirstOrDefault() != null
                              ? _fileService.GetFileUrl(product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameFileSystem, Contracts.File.UploadDirectory.Product)
                                  : String.Empty,
                                       model.Quantity,
                                       model.SizeId,
                                      _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                                             .Select(ps => new SizeListItemViewModel(ps.SizeId, ps.Size.PersonSize)).ToList(),
                                         model.SizeId != null
                                         ? _dataContext.Sizes.FirstOrDefault(s => s.Id == model.SizeId)!.PersonSize
                                         : _dataContext.Sizes.FirstOrDefault()!.PersonSize,
                                          product.DiscountPrice == null ? (decimal)sizePrice! : (decimal)sizeDiscountPrice!,
                                          product.DiscountPrice == null ? (decimal)sizePrice! * model.Quantity : (decimal)sizeDiscountPrice! * model.Quantity));
                }
                else
                {


                    if (cookieViewModel.DisCountPrice == 0)
                    {
                        cookieViewModel.Quantity = model.Quantity != null ? cookieViewModel.Quantity += model.Quantity : cookieViewModel.Quantity += 1;
                        cookieViewModel.Total = cookieViewModel.Quantity * cookieViewModel.Price;
                    }
                    else
                    {
                        cookieViewModel.Quantity = model.Quantity != null ? cookieViewModel.Quantity += model.Quantity : cookieViewModel.Quantity += 1;
                        cookieViewModel.Total = cookieViewModel.Quantity * cookieViewModel.DisCountPrice;
                    }

                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("wishList", JsonSerializer.Serialize(productCookieViewModel));

                return productCookieViewModel;
            };
        }
    }
}
