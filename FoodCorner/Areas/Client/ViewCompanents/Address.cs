using FoodCorner.Areas.Admin.ViewModels.Address;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Client.ViewCompanents
{

    [ViewComponent(Name = "Address")]
    public class Address : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Address(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Address.Include(c => c.User)
              .Where(u => u.User.Roles.Name.Equals("admin")).Take(1).Select(c => new ListItemViewModel(c.Id, c.City, c.Street, c.PhoneNum, c.User.Email)).ToListAsync();

            return View(model);

        }
    }
}
