using FoodCorner.Areas.Admin.ViewModels.Product;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Migrations;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Product = FoodCorner.Database.Models.Product;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService, ILogger<ProductController> logger)
        {
            _provider = provider;
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Products

                .Include(p=> p.ProductImages).OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(p.Id,p.Name,p.Description,p.Price,
                p.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages.Where(p=> p.IsPoster == true).FirstOrDefault().ImageNameFileSystem,UploadDirectory.Product)
                :String.Empty)).ToListAsync();

            

            return View(model);
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

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }

            await AddProduct();
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

                //model.Colors = _dataContext.Colors
                // .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                // .ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }

            async Task AddProduct()
            {
                var discountPrice = (model.Price * model.DiscountPercent) / 100;
                var lastPrice = model.Price - discountPrice;

                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    DiscountPercent = model.DiscountPercent,
                    DiscountPrice = lastPrice,
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

                ///////////////////////////////////////////////////////////////////
                foreach (var catagoryId in model.CategoryIds)
                {
                    var productCatagory = new ProductCatagory
                    {
                        CatagoryId = catagoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }
                foreach (var tagId in model.TagIds)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }
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

            var model = new UpdateViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPercent = product.DiscountPercent,
                DiscountPrice = product.DiscountPrice,

                ImagesUrl = product.ProductImages
                .Where(p=> p.IsPoster == false)
                .Select(p=> new UpdateViewModel.Images(p.Id,_fileService.GetFileUrl(p.ImageNameFileSystem,UploadDirectory.Product))).ToList(),

                PosterImgUrls = product.ProductImages.Where(p=> p.IsPoster == true)
                .Select(p=> new UpdateViewModel.PosterImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

                Catagories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.ProductCatagories.Select(pc => pc.CatagoryId).ToList(),

                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.PersonSize)).ToListAsync(),
                SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),

                //Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                //ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
                TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),

            };

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


            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }


            await UpdateProductAsync();

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


            async Task UpdateProductAsync()
            {
                var discountPrice = (model.Price * model.DiscountPercent) / 100;
                var lastPrice = model.Price - discountPrice;

                if (model.ProductImgIds is null)
                {
                    foreach (var item in product.ProductImages.Where(p=> p.IsPoster == false))
                    {
                        await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Product);
                        _dataContext.ProductImages.Remove(item);
                        await _dataContext.SaveChangesAsync();
                    }

                }
                if (model.ProductImgIds is not null)
                {
                    var removedImg = product.ProductImages.Where(p=>p.IsPoster == false).Where(pi => !model.ProductImgIds.Contains(pi.Id)).ToList();

                    foreach (var item in removedImg)
                    {
                        if (item.Id != 0)
                        {
                            var image = await _dataContext.ProductImages.FirstOrDefaultAsync(p => p.Id == item.Id);
                            await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Product);
                            _dataContext.ProductImages.Remove(item);
                            await _dataContext.SaveChangesAsync();
                        }
                    }
                }


                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.UpdateAt = DateTime.Now;
                product.DiscountPercent = model.DiscountPercent;
                product.DiscountPrice = lastPrice;

                #region Catagory
                var categoriesInDb = product.ProductCatagories.Select(bc => bc.CatagoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CatagoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCatagory = new ProductCatagory
                    {
                        CatagoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }
                #endregion

                #region Tag
                var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
                #endregion


                #region Size
                var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
                var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

                product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                #endregion


                #region Images
                if (model.PosterImage is not null)
                {
                    var productImg = await _dataContext.ProductImages.Where(p => p.IsPoster == true).FirstOrDefaultAsync(p=> p.ProductId == product.Id);
                    await _fileService.DeleteAsync(productImg.ImageNameFileSystem, UploadDirectory.Product);
                    _dataContext.ProductImages.Remove(productImg);

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
                #endregion        
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
    }
}
