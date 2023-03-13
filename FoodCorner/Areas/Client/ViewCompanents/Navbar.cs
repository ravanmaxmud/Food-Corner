using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;

namespace FoodCorner.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "Navbar")]
    public class Navbar : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        public Navbar(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.Navbars.Include
                (n => n.SubNavbars.OrderBy
                (sn => sn.Order)).Where(n=> n.IsViewHeader == true).OrderBy(n => n.Order).ToList();

            return View(model);
        }
    }
}
