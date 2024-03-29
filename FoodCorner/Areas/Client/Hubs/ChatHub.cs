﻿using FoodCorner.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodCorner.Areas.Client.Hubs
{
    [Authorize]
    public class ChatHub :Hub
    {
        public async Task SendMessage(string message, string userId)
        {

            await Clients.Clients(userId).SendAsync("ReceiveMessage", message, Context.ConnectionId);
            await Clients.Clients(Context.ConnectionId).SendAsync("OwnMessage", message.Trim());
        }
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Clients.All.SendAsync("OnlineUserList", connectionId);
            return base.OnConnectedAsync();
        }
        public async Task OnlineUsers()
        {
            var connectionId = Context.ConnectionId;
            await Clients.All.SendAsync("OnlineUserList", connectionId);
        }
    }
}
