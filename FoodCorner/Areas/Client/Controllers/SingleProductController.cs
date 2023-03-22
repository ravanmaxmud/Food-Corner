
using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Areas.Client.ViewModels.Home.Modal;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static FoodCorner.Areas.Client.ViewModels.Home.Modal.ModalViewModel;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("singleProduct")]
    public class SingleProductController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public SingleProductController(DataContext dataContext, IFileService fileService, IActionDescriptorCollectionProvider provider, IUserService userService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _provider = provider;
            _userService = userService;
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
                .Select(ps => new ModalViewModel.TagViewModeL(ps.Tag.Title, ps.Tag.Id)).ToList(),

                  _dataContext.Comments.Where(pc=> pc.ProductId == product.Id)
                  .Select(pc=> new ModalViewModel.CommentViewModel(pc.Id,pc.Content,$"{pc.User.FirstName} {pc.User.LastName}")).ToList()
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

        [HttpPost("comment/{productId}", Name = "client-single-comment")]
        public async Task<IActionResult> Comment(int productId, ModalViewModel model) 
        {
            if (!_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-auth-login");
            }
            if (model.AddComments.Content is null)
            {
                return RedirectToRoute("client-single-index", new { id = productId });
            }
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) { return NotFound(); }

            var comment = new Comment
            {
                ProductId = product.Id,
                UserId = _userService.CurrentUser.Id,
                Content = model.AddComments.Content

            };

            await _dataContext.Comments.AddAsync(comment);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("client-single-index", new { id = product.Id });
        }
    }
}
