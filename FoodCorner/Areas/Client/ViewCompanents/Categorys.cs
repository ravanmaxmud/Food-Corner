using FoodCorner.Areas.Client.ViewModels.Home;
using FoodCorner.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "Categorys")]
    public class Categorys : ViewComponent
    {

        private readonly DataContext _dataContext;

        public Categorys(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = 
                await _dataContext.Categories
                .Select(c=> new CategoryViewModel(c.Id,c.Title)).ToListAsync();
                

            return View(model);
        }
    }
}
