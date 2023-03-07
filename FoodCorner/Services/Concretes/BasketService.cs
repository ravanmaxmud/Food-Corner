﻿using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Client.ViewModels.Basket;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using System.Text.Json;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using Microsoft.AspNetCore.Mvc;
using FoodCorner.Areas.Client.ViewCompanents;
using Microsoft.IdentityModel.Tokens;

namespace FoodCorner.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        //private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;



        public BasketService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product, ModalViewModel model)
        {
            model = new ModalViewModel
            {
                SizeId = model.SizeId != null ? model.SizeId : _dataContext.Sizes.FirstOrDefault().Id,
                Quantity = model.Quantity != null ? model.Quantity : 1
            };
            //if (_userService.IsAuthenticated)
            //{
            //    await AddToDatabaseAsync();
            //    return new List<BasketCookieViewModel>();
            //}

            return AddCookie();


            //async Task AddToDatabaseAsync() 
            //{
            //    var basketProduct = await _dataContext.BasketProducts
            //         .Include(b => b.Basket)
            //         .FirstOrDefaultAsync(bp => bp.Basket.User.Id == _userService.CurrentUser.Id && bp.ProductId == product.Id);

            //    if (basketProduct is not null)
            //    {
            //        basketProduct.Quantity++;
            //    }
            //    else
            //    {
            //        var basket = await _dataContext.Baskets.FirstAsync(p =>p.UserId == _userService.CurrentUser.Id);

            //        basketProduct = new BasketProduct 
            //        {
            //            Quantity =1,
            //            BasketId = basket.Id,
            //            ProductId = product.Id
            //        };

            //        await _dataContext.BasketProducts.AddAsync(basketProduct);
            //    }
            //        await _dataContext.SaveChangesAsync();
            //}

            List<BasketCookieViewModel> AddCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];

                var productCookieViewModel = productCookieValue is not null
                   ? JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue)
                   : new List<BasketCookieViewModel> { };

                var cookieViewModel = productCookieViewModel!.FirstOrDefault(pc => pc.Id == product.Id && pc.SizeId == model.SizeId);

                if (cookieViewModel is null || cookieViewModel.SizeId != model.SizeId)
                {
                    productCookieViewModel.Add
                           (new BasketCookieViewModel(product.Id, product.Name, product.ProductImages.Take(1).FirstOrDefault() != null
                              ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault().ImageNameFileSystem, Contracts.File.UploadDirectory.Product)
                                  : String.Empty,
                                       model.Quantity,
                                       model.SizeId,
                                      _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                                             .Select(ps => new SizeListItemViewModel(ps.SizeId, ps.Size.PersonSize)).ToList(),
                                         model.SizeId != null
                                         ? _dataContext.Sizes.FirstOrDefault(s => s.Id == model.SizeId).PersonSize
                                         : _dataContext.Sizes.FirstOrDefault().PersonSize,
                                          product.DiscountPrice == null ? (decimal)product.Price : (decimal)product.DiscountPrice, 
                                          product.DiscountPrice == null ? (decimal)product.Price * model.Quantity  : (decimal)product.DiscountPrice * model.Quantity));
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

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));

                return productCookieViewModel;
            };
        }


    }
}
