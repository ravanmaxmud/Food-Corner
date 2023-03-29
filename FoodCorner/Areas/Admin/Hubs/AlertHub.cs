using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodCorner.Areas.Admin.Hubs
{
    [Authorize(Roles = "admin")]
    public class AlertHub : Hub
    {
    }
}
