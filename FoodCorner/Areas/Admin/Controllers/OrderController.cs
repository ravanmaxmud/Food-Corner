using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Admin.ViewModels.Order;
using FoodCorner.Contracts.Email;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using FoodCorner.Contracts.File;
using FoodCorner.Services.Concretes;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/orders")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        public IEmailService _emailService { get; set; }
        public OrderController(DataContext dataContext, IUserService userService, IEmailService emailService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("list",Name ="admin-order-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Orders
               .Select(b => new ListItemViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
               .ToListAsync();

            return View(model);
        }


        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> Update(string id)
        {
            var order = await _dataContext.Orders.Include(o=> o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel { Id = id };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> Update(string id,UpdateViewModel model)
        {
            var order = await _dataContext.Orders.Include(p=> p.User).Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }
            order.Status = model.Status;

            var stausMessageDto = PrepareStausMessage(order.User.Email);
            _emailService.Send(stausMessageDto);

            if (order.Status == Database.Models.Enums.OrderStatus.Completed || order.Status == Database.Models.Enums.OrderStatus.Rejected)
            {
                _dataContext.Orders.Remove(order);
                await _dataContext.SaveChangesAsync();
                return RedirectToRoute("admin-order-list");
            }
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");
            MessageDto PrepareStausMessage(string email)
            {
                string body = "Order Has Been Updated";

                string subject = EmailMessages.Subject.NOTIFICATION_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-order-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var orders = await _dataContext.Orders.Include(p=> p.OrderProducts).FirstOrDefaultAsync(p => p.Id == id);
            if (orders is null)
            {
                return NotFound();
            }
            _dataContext.Orders.Remove(orders);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-order-list");
        }
    }
}
