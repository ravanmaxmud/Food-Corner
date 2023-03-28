using FoodCorner.Areas.Admin.ViewModels.Address;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contactUs")]
    [Authorize(Roles = "admin")]
    public class AddressController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AddressController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        [HttpGet("list",Name ="admin-contactus-list")]
        public async Task <IActionResult> List()
        {
            var model = await _dbContext.Address.Include(c=>c.User)
                .Where(u=> u.User.Roles.Name.Equals("admin")).Select(c=> new ListItemViewModel(c.Id,c.City,c.Street,c.PhoneNum,c.User.Email)).ToListAsync();

            return View(model);
        }

        [HttpGet("update/{id}", Name = "admin-contactus-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var address = await _dbContext.Address.FirstOrDefaultAsync(a=>a.Id == id);
            if (address == null) { return NotFound(); }
            var model = new UpdateViewModel
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                PhoneNumber = address.PhoneNum,

            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-contactus-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var address = await _dbContext.Address.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (address == null) { return NotFound(); }

            if (!ModelState.IsValid)
            {
                 model = new UpdateViewModel
                {
                    Id = address.Id,
                    City = address.City,
                    Street = address.Street,
                    PhoneNumber = address.PhoneNum,

                };
                return View(model);
            }

            address.City = model.City;
            address.Street = model.Street;
            address.PhoneNum = model.PhoneNumber;

            await _dbContext.SaveChangesAsync();
           
            return RedirectToRoute("admin-contactus-list");
        }
        [HttpPost("delete/{id}", Name = "admin-contactus-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var address = await _dbContext.Address.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null) { return NotFound(); }

            _dbContext.Address.Remove(address);
            await _dbContext.SaveChangesAsync();


            return RedirectToRoute("admin-contactus-list");
        }
    }
}
