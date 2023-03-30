using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    [Authorize(Roles = "admin")]
    public class AdminHomeController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public AdminHomeController(DataContext dataContext, IUserService userService, INotificationService notificationService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _notificationService = notificationService;
        }


        [HttpGet("index",Name ="admin-home-index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("updateMessage/{id}", Name = "admin-home-updateMessage")]
        public async Task<IActionResult> UpdateMessage([FromRoute] int id)
        {
            var message = await _dataContext.Messages.FirstOrDefaultAsync(m=> m.Id == id); 
            if (message == null) { return NotFound(); }

            message.IsSeen = true;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");
        }
    }
}
