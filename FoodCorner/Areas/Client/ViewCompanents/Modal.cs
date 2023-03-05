using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{

    //[ViewComponent(Name = "Modal")]
    //public class Modal : ViewComponent
    //{

    //    //private readonly DataContext _dataContext;
    //    //private readonly IFileService _fileService;

    //    //public Modal(DataContext dataContext, IFileService fileService)
    //    //{
    //    //    _dataContext = dataContext;
    //    //    _fileService = fileService;
    //    //}

    //    //public async Task<IViewComponentResult> InvokeAsync(Product? product)
    //    //{
    //    //    var model = new ModalViewModel(product.Id, product.Name, product.Description, product.Price,
    //    //         product.ProductImages
    //    //          .Select(p => new ModalViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

    //    //         _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
    //    //         .Select(ps => new ModalViewModel.SizeViewModeL(ps.Size.PersonSize, ps.Size.Id)).ToList()
    //    //         );


    //    //    return View(model);
    //    //}
    //}
}
