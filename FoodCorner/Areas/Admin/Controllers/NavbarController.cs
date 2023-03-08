using FoodCorner.Areas.Admin.ViewModels.Navbar;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/nav")]
    public class NavbarController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _provider = provider;
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
            var model = new AddViewModel
            {
                Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new AddViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-nav-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new AddViewModel
                {
                    Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                  .Select(u => new AddViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
                };
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


        #region Update

        [HttpGet("update/{id}", Name = "admin-nav-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navItem is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = navItem.Id,
                Name = navItem.Name,
                ToURL = navItem.ToURL,
                Order = navItem.Order,
                IsViewHeader = navItem.IsViewHeader,
                IsViewFooter = navItem.IsViewFooter,
                Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new UpdateViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };
            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-nav-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var navItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navItem is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                 model = new UpdateViewModel
                {
                    Id = navItem.Id,
                    Name = navItem.Name,
                    ToURL = navItem.ToURL,
                    Order = navItem.Order,
                    IsViewHeader = navItem.IsViewHeader,
                    IsViewFooter = navItem.IsViewFooter,
                    Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
               .Select(u => new UpdateViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
                };

                return View(model);
            }


            navItem.Name = model.Name;
            navItem.Order = model.Order;
            navItem.ToURL = model.ToURL;
            navItem.IsViewHeader = model.IsViewHeader;
            navItem.IsViewFooter = model.IsViewFooter;


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