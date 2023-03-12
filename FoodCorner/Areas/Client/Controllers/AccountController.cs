using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            //var model = await _dataContext.Orders.Where(o => o.UserId == _userService.CurrentUser.Id)
            //      .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
            //      .ToListAsync();




            return View();
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
        public IActionResult Address()
        {
            return View();
        }

        [HttpGet("details", Name = "client-account-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
