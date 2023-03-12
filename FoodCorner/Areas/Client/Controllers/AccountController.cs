using FoodCorner.Areas.Client.ViewModels.Account;
using FoodCorner.Database;
using FoodCorner.Database.Models;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Client.ViewModels.Account;
using System.IO;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet("order", Name = "client-account-order")]
        public async Task<IActionResult> Order()
        {
            var model = await _dataContext.Orders.Where(o => o.UserId == _userService.CurrentUser.Id)
                  .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
                  .ToListAsync();


            return View(model);
        }

        [HttpGet("specialOrder", Name = "client-account-specialOrder")]
        public async Task<IActionResult> SpecialOrder()
        {
            //var model = await _dataContext.Orders.Where(o => o.UserId == _userService.CurrentUser.Id)
            //      .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
            //      .ToListAsync();




            return View();
        }

        [HttpGet("address", Name = "client-account-address")]
        public async Task<IActionResult> Address()
        {
            var user = _userService.CurrentUser;

            var address = await _dataContext.Address.FirstOrDefaultAsync(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-edit-address", new EditAddressViewModel());
            }

            var model = new AddressListViewModel
            {
                User = $"{address.User.FirstName} {address.User.LastName}",
                City = address.City,
                Street = address.Street,
                PhoneNumber = address.PhoneNum,
            };
            return View(model);
        }

        [HttpGet("editAddress", Name = "client-account-edit-address")]
        public async Task<IActionResult> EditAddress()
        {
            var user = _userService.CurrentUser;

            var address = await _dataContext.Address.FirstOrDefaultAsync(a => a.UserId == user.Id);

            if (address is null)
            {
                return View(new EditAddressViewModel());
            }


            var model = new EditAddressViewModel
            {
                PhoneNumber = address.PhoneNum,
                City = address.City,
                Street = address.Street
            };
            return View(model);
        }

        [HttpPost("editAddress", Name = "client-account-edit-address")]
        public async Task<IActionResult> EditAddress(EditAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
             
            var user = _userService.CurrentUser;

            var address = await _dataContext.Address.FirstOrDefaultAsync(a => a.UserId == user.Id);



            if (address is not null)
            {
                address.PhoneNum = model.PhoneNumber;
                address.City = model.City;
                address.Street = model.Street;

            }
            else
            {
                var newAddress = new Addres
                {
                    UserId = user.Id,
                    PhoneNum = model.PhoneNumber,
                    City = model.City,
                    Street = model.Street
                };
                await _dataContext.Address.AddAsync(newAddress);
            }

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-address");
        }


        [HttpGet("details", Name = "client-account-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
