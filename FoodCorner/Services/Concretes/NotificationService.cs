using FoodCorner.Areas.Admin.Hubs;
using FoodCorner.Contracts.Alert;
using FoodCorner.Contracts.Identity;
using FoodCorner.Database;
using FoodCorner.Services.Abstracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Services.Concretes
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<AlertHub> _alertHub;
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;

        public NotificationService(IHubContext<AlertHub> alertHub, IUserService userService, DataContext dataContext)
        {
            _alertHub = alertHub;
            _userService = userService;
            _dataContext = dataContext;
        }

        public async Task SendOrderCreatedToAdmin(string orderId)
        {
            foreach (var user in await _dataContext.Users.Where(u => u.Roles.Name == RoleNames.ADMIN).ToListAsync())
            {
                await _alertHub.Clients
                    .Group(user.Id.ToString())
                    .SendAsync("Notify", new
                    {
                        Title = AlertMessages.ORDER_CREATED_TITLE_TO_MODERATOR,
                        Content = AlertMessages.ORDER_CREATED_CONTENT_TO_MODERATOR
                                    .Replace("{user_email}", _userService.CurrentUser.Email)
                                    .Replace("{tracking_code}", orderId)  //string builder should be used
                    });
            }

            await _alertHub.Clients
                    .Group(_userService.CurrentUser.Id.ToString())
                     .SendAsync("Notify", new
                    {
                        Title = AlertMessages.ORDER_CREATED_TITLE_TO_OWNER,
                           Content = AlertMessages.ORDER_CREATED_CONTENT_TO_OWNER
                            .Replace("{tracking_code}", orderId)  //string builder should be used
                      });
        }
    }
}
