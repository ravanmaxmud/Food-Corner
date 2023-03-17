using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Contracts.File;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{
	[ViewComponent(Name = "ShopPageProduct")]
	public class ShopPageProduct : ViewComponent
	{
		private readonly DataContext _dataContext;
		private readonly IFileService _fileService;
		public ShopPageProduct(DataContext dataContext, IFileService fileService)
		{
			_dataContext = dataContext;
			_fileService = fileService;
		}

		public async Task<IViewComponentResult> InvokeAsync([FromQuery] int? sort = null, [FromQuery] int? categoryId = null,int? minPrice =null , int? maxPrice = null, [FromQuery] int? tagId = null)
		{
			var productsQuery = _dataContext.Products.Include(p => p.ProductCatagories).AsQueryable();

			if (sort is not null)
			{
				switch (sort)
				{
					case 1:
						productsQuery = productsQuery.OrderBy(p => p.Name);
						break;

					case 2:
						productsQuery = productsQuery.OrderByDescending(p => p.Name);
						break;
					case 3:
						productsQuery = productsQuery.OrderBy(p => p.CreatedAt);
						break;
					case 4:
						productsQuery = productsQuery.OrderBy(p => p.Price);
						break;
					case 5:
						productsQuery = productsQuery.OrderByDescending(p => p.Price);
						break;
				}
			}
			else if (categoryId is not null || tagId is not null)
			{
				productsQuery = productsQuery.Include(p => p.ProductCatagories).Include(p=> p.ProductTags)
					.Where(p => categoryId == null || p.ProductCatagories!.Any(pc => pc.CatagoryId == categoryId))
					.Where(p=> tagId == null || p.ProductTags!.Any(pt=> pt.TagId == tagId));
			}
            else if (minPrice is not null && maxPrice is not null)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            else
			{
				productsQuery = productsQuery.OrderBy(p => p.Price);
			}


			var model = await productsQuery
				.Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price, p.DiscountPercent, p.DiscountPrice, p.CreatedAt,
				 p.ProductImages!.Where(p => p.IsPoster == true).FirstOrDefault() != null
				 ? _fileService.GetFileUrl(p.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Product)
				 : String.Empty)).ToListAsync();

			return View(model);
		}
	}
}
