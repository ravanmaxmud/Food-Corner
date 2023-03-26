using FoodCorner.Areas.Admin.ViewModels.Product;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Migrations;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Product = FoodCorner.Database.Models.Product;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService, ILogger<ProductController> logger, IProductService productService)
        {
            _provider = provider;
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List(string? search = null, string? searchBy = null, int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_dataContext.Products.Count() / 6);
            return View(await _productService.GetAllProduct(search,searchBy));
        }


        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Catagories = await _dataContext.Categories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                    .ToListAsync(),

                Tags = await _dataContext.Tags
                .Select(t=> new TagListItemViewModel(t.Id,t.Title))
                .ToListAsync(),

                Sizes = await _dataContext.Sizes
                .Select(S=> new SizeListItemViewModel(S.Id,S.PersonSize))
                .ToListAsync()
            };

            return View(model);
        }


        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }
            var cehckCategory = await _productService.CheckProductCategory(model.CategoryIds,ModelState);
            var checkTag = await _productService.CheckProductTag(model.TagIds, ModelState);
            var cehckSize = await _productService.CheckProductSize(model.SizeIds, ModelState);
            if (!cehckCategory || !checkTag || !cehckSize)
            {
                return GetView(model);
            }
            await _productService.AddProduct(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            IActionResult GetView(AddViewModel model)
            {

                model.Catagories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.PersonSize))
                 .ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }
        }

        #region Update
        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(p=> p.ProductSizes)
                .Include(p=> p.ProductTags)
                .Include(p=>p.ProductCatagories)
                .Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }
            var model = await _productService.GetUpdatedProduct(product,id);
            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel? model)
        {
            var product = await _dataContext.Products
                    .Include(p => p.ProductSizes)
                    .Include(p=> p.ProductTags)
                    .Include(p=>p.ProductCatagories)
                    .Include(p=> p.ProductImages).FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }
            var cehckCategory = await _productService.CheckProductCategory(model.CategoryIds, ModelState);
            var checkTag = await _productService.CheckProductTag(model.TagIds, ModelState);
            var cehckSize = await _productService.CheckProductSize(model.SizeIds, ModelState);
            if (!cehckCategory || !checkTag || !cehckSize)
            {
                return GetView(model);
            }
            await _productService.UpdateProduct(product,model);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");


            #region GetviewAndUpdate
            IActionResult GetView(UpdateViewModel model)
            {
                model.ImagesUrl = product.ProductImages
               .Where(p => p.IsPoster == false)
               .Select(p => new UpdateViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList();

               model.PosterImgUrls = product.ProductImages.Where(p => p.IsPoster == true)
                .Select(p => new UpdateViewModel.PosterImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                model.Catagories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = product.ProductCatagories.Select(c => c.CatagoryId).ToList();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.PersonSize))
                 .ToList();

                 model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }
            #endregion
        }
        #endregion




        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Products.Include(p=>p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (products is null)
            {
                return NotFound();
            }

            foreach (var item in products.ProductImages)
            {
                await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Product);
            }
            _dataContext.Products.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");
        }
        #endregion

        #region ProductComments

        [HttpGet("commentList/{id}", Name = "admin-product-commentList")]
        public async Task<IActionResult> CommentList([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) { return NotFound();}

            var model = await _dataContext.Comments.Where(pc=> pc.ProductId ==product.Id)
                .Select(pc=> new ProductCommentList(pc.Id,pc.Content,$"{pc.User.FirstName} {pc.User.LastName}",pc.IsAccepted)).ToListAsync();

            return View(model);
        }

        [HttpPost("acceptComment/{commentId}", Name = "admin-product-acceptComment")]
        public async Task<IActionResult> AcceptComment([FromRoute] int commentId)
        {

            var comment = await _dataContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment is null)
            {
                return NotFound();
            }

            comment.IsAccepted = true;
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-commentList", new { id = comment.ProductId });
        }

        [HttpPost("deleteComment/{commentId}", Name = "admin-product-deleteComment")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {

            var comment = await _dataContext.Comments.FirstOrDefaultAsync(c=> c.Id == commentId);
            if (comment is null)
            {
                return NotFound();
            }

            _dataContext.Comments.Remove(comment);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-commentList", new { id = comment.ProductId });
        }
        #endregion
    }
}
