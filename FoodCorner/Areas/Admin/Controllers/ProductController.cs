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



        #region Update
        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
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

                ImagesUrl = product.ProductImages
                .Where(p=> p.IsPoster == false)
                .Select(p=> new UpdateViewModel.Images(p.Id,_fileService.GetFileUrl(p.ImageNameFileSystem,UploadDirectory.Product))).ToList(),

                PosterImgUrls = product.ProductImages.Where(p=> p.IsPoster == true)
                .Select(p=> new UpdateViewModel.PosterImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList()

                //Categories = await _dataContext.Catagories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                //CategoryIds = product.ProductCatagories.Select(pc => pc.CatagoryId).ToList(),

                //Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Title)).ToListAsync(),
                //SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),

                //Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                //ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

                //Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
                //TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),

            };

            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel? model)
        {
            var product = await _dataContext.Products
                    .Include(p=> p.ProductImages).FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }


            //foreach (var categoryId in model.CategoryIds)
            //{
            //    if (!await _dataContext.Catagories.AnyAsync(c => c.Id == categoryId))
            //    {
            //        ModelState.AddModelError(string.Empty, "Something went wrong");
            //        _logger.LogWarning($"Category with id({categoryId}) not found in db ");
            //        return GetView(model);
            //    }

            //}

            //foreach (var sizeId in model.SizeIds)
            //{
            //    if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
            //    {
            //        ModelState.AddModelError(string.Empty, "Something went wrong");
            //        _logger.LogWarning($"Size with id({sizeId}) not found in db ");
            //        return GetView(model);
            //    }

            //}

            //foreach (var colorId in model.ColorIds)
            //{
            //    if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
            //    {
            //        ModelState.AddModelError(string.Empty, "Something went wrong");
            //        _logger.LogWarning($"Color with id({colorId}) not found in db ");
            //        return GetView(model);
            //    }

            //}

            //foreach (var tagId in model.TagIds)
            //{
            //    if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
            //    {
            //        ModelState.AddModelError(string.Empty, "Something went wrong");
            //        _logger.LogWarning($"Tag with id({tagId}) not found in db ");
            //        return GetView(model);
            //    }

            //}


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

                //model.Categories = _dataContext.Catagories
                //   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                //   .ToList();

                //model.CategoryIds = product.ProductCatagories.Select(c => c.CatagoryId).ToList();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //model.Sizes = _dataContext.Sizes
                // .Select(c => new SizeListItemViewModel(c.Id, c.Title))
                // .ToList();

                //model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //model.Colors = _dataContext.Colors
                // .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                // .ToList();

                //model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //model.Tags = _dataContext.Tags
                // .Select(c => new TagListItemViewModel(c.Id, c.Title))
                // .ToList();

                //model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }
            async Task UpdateProductAsync()
            {

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

                //#region Catagory
                //var categoriesInDb = product.ProductCatagories.Select(bc => bc.CatagoryId).ToList();
                //var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                //var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                //product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CatagoryId));

                //foreach (var categoryId in categoriesToAdd)
                //{
                //    var productCatagory = new ProductCatagory
                //    {
                //        CatagoryId = categoryId,
                //        Product = product,
                //    };

                //    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                //}
                //#endregion

                //#region Color
                //var colorInDb = product.ProductColors.Select(bc => bc.ColorId).ToList();
                //var colorToRemove = colorInDb.Except(model.ColorIds).ToList();
                //var colorToAdd = model.ColorIds.Except(colorInDb).ToList();

                //product.ProductColors.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


                //foreach (var colorId in colorToAdd)
                //{
                //    var productColor = new ProductColor
                //    {
                //        ColorId = colorId,
                //        Product = product,
                //    };

                //    await _dataContext.ProductColors.AddAsync(productColor);
                //}
                //#endregion


                //#region Size
                //var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
                //var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
                //var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

                //product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                //foreach (var sizeId in sizeToAdd)
                //{
                //    var productSize = new ProductSize
                //    {
                //        SizeId = sizeId,
                //        Product = product,
                //    };

                //    await _dataContext.ProductSizes.AddAsync(productSize);
                //}

                //#endregion

                //#region Tag
                //var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
                //var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                //var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                //product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                //foreach (var tagId in tagToAdd)
                //{
                //    var productTag = new ProductTag
                //    {
                //        TagId = tagId,
                //        Product = product,
                //    };

                //    await _dataContext.ProductTags.AddAsync(productTag);
                //}
                //#endregion
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
