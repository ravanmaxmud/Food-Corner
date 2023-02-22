using FoodCorner.Areas.Admin.ViewModels.Navbar;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/nav")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region List

        [HttpGet("list", Name = "admin-nav-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Navbars
                .Select
                (n => new ListItemViewModel
                (n.Id, n.Name, n.ToURL, n.Order, n.IsViewHeader, n.IsViewFooter))
                .ToListAsync();

            return View(model);
        }


        #endregion

        #region Add

        [HttpGet("add", Name = "admin-nav-add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-nav-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_dataContext.Navbars.Any(a => a.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "Order is not be the same");
                return View(model);
            }

            var navBar = new Navbar
            {
                Name = model.Name,
                ToURL = model.ToURL,
                Order = model.Order,
                IsViewFooter = model.IsViewFooter,
                IsViewHeader = model.IsViewHeader,
            };

            await _dataContext.Navbars.AddAsync(navBar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-nav-list");
        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-nav-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navItem is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navItem);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-nav-list");
        }

        #endregion
    }
}