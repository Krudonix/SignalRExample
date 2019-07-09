using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var http = this.Context.GetHttpContext();
            var user = http.Request.Query["user"];

            Users.TryAdd(this.Context.ConnectionId, user);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Users.TryRemove(this.Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task ChatAll(string message)
        {
            Users.TryGetValue(this.Context.ConnectionId, out string user);
            await this.Clients.All.SendAsync("Chat", user, message);
        }

        public async Task ChatDirect(string user, string message)
        {
            await this.Clients.All.SendAsync("Chat", message);
        }

        public Dictionary<string, string> GetUserData()
        {
            return Users.ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
