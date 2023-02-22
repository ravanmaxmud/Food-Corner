using FoodCorner.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{

    [ViewComponent(Name = "Footer")]
    public class Footer : ViewComponent
    {

        private readonly DataContext _dataContext;

        public Footer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.Navbars.Include
                (n => n.SubNavbars.OrderBy
                (sn => sn.Order)).Where(n => n.IsViewFooter == true).OrderBy(n => n.Order).ToList();

            return View(model);
        }
    }
}
