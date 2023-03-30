using FoodCorner.Areas.Admin.ViewModels.Alert;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Areas.Admin.ViewCompanents
{

    [ViewComponent(Name = "ShowAlert")]
    public class ShowAlert : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public ShowAlert(DataContext dataContext, IUserService userService, INotificationService notificationService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var alertMessages = await _dataContext.Messages
                .Where(m=> m.UserId == _userService.CurrentUser.Id)
                .Where(m=> m.IsSeen == false)
                .Select(m=> new ShowAlertViewModel 
                {
                    Id = m.Id,
                    Title = m.Title,
                    Content = m.Content,
                    CreatedAt = m.CreatedAt,
                }).ToListAsync();

            return View(alertMessages);

        }
    }
}
