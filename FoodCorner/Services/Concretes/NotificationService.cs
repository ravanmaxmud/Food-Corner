using FoodCorner.Areas.Admin.Hubs;
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
            string title = "New Order Created! Please Chechk";
            string content = $"{_userService.CurrentUser.Email} Created New Order {orderId}";

            await _alertHub.Clients.All
                               .SendAsync("Notify", new { Title = title, Content = content });

        }
    }
}
