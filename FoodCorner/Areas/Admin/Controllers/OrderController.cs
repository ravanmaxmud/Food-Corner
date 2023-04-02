using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Areas.Admin.ViewModels.Order;
using FoodCorner.Contracts.Email;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using FoodCorner.Contracts.File;
using FoodCorner.Services.Concretes;
using Microsoft.AspNetCore.Hosting;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/orders")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public IEmailService _emailService { get; set; }
		private readonly IWebHostEnvironment _webHostEnvironment;
		public OrderController(DataContext dataContext, IUserService userService, IEmailService emailService, IFileService fileService, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = dataContext;
			_userService = userService;
			_emailService = emailService;
			_fileService = fileService;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet("list",Name ="admin-order-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Orders.Include(o=> o.User)
               .Select(b => new ListItemViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice, b.User.Email,$"{b.User.Address.City} {b.User.Address.Street} {b.User.Address.PhoneNum}"))
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

            //if (order.Status == Database.Models.Enums.OrderStatus.Completed || order.Status == Database.Models.Enums.OrderStatus.Rejected)
            //{
            //    _dataContext.Orders.Remove(order);
            //    await _dataContext.SaveChangesAsync();
            //    return RedirectToRoute("admin-order-list");
            //}
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");


            MessageDto PrepareStausMessage(string email)
            {
				var pathToFile = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() +
			   "Client" + Path.DirectorySeparatorChar.ToString() + "EmailTempalte" +
			    Path.DirectorySeparatorChar.ToString() + "OrderStatus.html";


				string body = "";
				using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
				{
					body = streamReader.ReadToEnd();
				}

				string messageBody = string.Format(body);

				string subject = EmailMessages.Subject.NOTIFICATION_MESSAGE;

                return new MessageDto(email, subject, messageBody);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-order-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var orders = await _dataContext.Orders.Include(p => p.OrderProducts).FirstOrDefaultAsync(p => p.Id == id);
            if (orders is null)
            {
                return NotFound();
            }
            _dataContext.Orders.Remove(orders);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-order-list");
        }
        [HttpGet("list/{id}", Name = "admin-orderProduct-list")]
        public async Task<IActionResult> OrderProductList(string id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            var model = await _dataContext.OrderProducts.Where(o=> o.OrderId == order.Id)
                .Select(op => new OrderProductListItemViewModel(op.Id, op.Product.ProductImages
                .Where(p => p.IsPoster == true).FirstOrDefault() != null
              ? _fileService.GetFileUrl(op.Product.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault().ImageNameFileSystem, UploadDirectory.Product)
              : String.Empty, op.Product.Name, op.Size.PersonSize, op.Quantity, (int)op.Total)).ToListAsync();


            return View(model);
        }
    }
}
