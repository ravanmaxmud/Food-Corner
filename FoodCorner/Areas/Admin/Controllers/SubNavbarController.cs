using FoodCorner.Areas.Admin.ViewModels.SubNavbars;
using FoodCorner.Database.Models;
using FoodCorner.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/subnav")]
    [Authorize(Roles = "admin")]

    public class SubNavbarController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;

        public SubNavbarController(DataContext dataContext, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _provider = provider;
        }

        #region List

        [HttpGet("list", Name = "admin-subnav-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.SubNavbars
                .Select
                (s => new ListItemViewModel
                (s.Id, s.Name, s.ToURL, s.Order, s.Navbar.Name))
                .ToListAsync();
            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-subnav-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync(),
                Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new AddViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-subnav-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync(),
                    Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                  .Select(u => new AddViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
                };
                return View(model);
            }
            if (!await _dataContext.Navbars.AnyAsync(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Is Not Found");
                return View(model);
            }
            if (await _dataContext.SubNavbars.AnyAsync(a => a.Order == model.Order))
            {
                var navModel = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
                };
                ModelState.AddModelError(String.Empty, "Order is not be the same");
                return View(navModel);
            }

            var subNavbar = new SubNavbar
            {
                Name = model.Name,
                ToURL = model.ToURL,
                NavbarId = model.NavbarId,
                Order = model.Order,

            };
            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnav-list");
        }
        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-subnav-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNav == null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = subNav.Id,
                Name = subNav.Name,
                ToURL = subNav.ToURL,
                Order = subNav.Order,
                NavbarId = subNav.NavbarId,
                Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList(),
                Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new UpdateViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };
            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-subnav-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == model.Id);
            if (subNav is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Is Not Found");
                return View(model);
            }
            if (_dataContext.SubNavbars.Any(o=> o.Order != model.Order))
            {
                if (_dataContext.SubNavbars.Any(a => a.Order == model.Order))
                {
                    var navModel = new UpdateViewModel
                    {
                        Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync(),
                        Urls = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                        .Select(u => new UpdateViewModel.UrlViewModel(u!.AttributeRouteInfo.Template)).ToList()
                    };
                    ModelState.AddModelError(String.Empty, "Order is not be the same");
                    return View(navModel);
                }
            }

            subNav.Name = model.Name;
            subNav.Order = model.Order;
            subNav.NavbarId = model.NavbarId;
            subNav.ToURL = model.ToURL;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnav-list");
        }
        #endregion

        [HttpPost("delete/{id}", Name = "admin-subnav-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subNav = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNav == null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(subNav);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnav-list");
        }
    }
}
