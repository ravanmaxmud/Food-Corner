using FoodCorner.Areas.Client.ViewModels.ContactUs;
using FoodCorner.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contactus")]
    public class ContactUsController : Controller
    {
        private readonly DataContext _dataContext;

        public ContactUsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("index",Name = "client-contactus-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Addresses = await _dataContext.Address.Include(c => c.User)
              .Where(u => u.User.Roles.Name.Equals("admin")).Take(1).Select(c => new AddressViewModel(c.City, c.Street, c.PhoneNum, c.User.Email)).ToListAsync()
            };

            return View(model);
        }
    }
}
