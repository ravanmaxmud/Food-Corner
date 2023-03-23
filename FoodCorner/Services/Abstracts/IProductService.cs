using FoodCorner.Areas.Admin.ViewModels.Product;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FoodCorner.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ListItemViewModel>> GetAllProduct(string? search = null, string? searchBy = null);
        Task AddProduct(AddViewModel model);
        Task<bool> CheckProductSize(List<int> SizeIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductTag(List<int> TagIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductCategory(List<int> CategoryIds, ModelStateDictionary ModelState);
        Task<UpdateViewModel> GetUpdatedProduct(Product product,int id);
        Task UpdateProduct(Product product,UpdateViewModel model);

    }
}
