using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client
{
    public class SignalRClient
    {
        public event EventHandler<ChatEventArgs> ChatRecieved;
        public static SignalRClient Current { get; } = new SignalRClient();

        private HubConnection connection;

        public SignalRClient()
        {
        }

        private void OnChatRecieved(string from, string message)
        {
            this.ChatRecieved?.Invoke(this, new ChatEventArgs(from, message));
        }

        public async Task Connect(string user)
        {
            if (this.connection != null)
                await this.connection.StopAsync();

            this.connection = new HubConnectionBuilder().WithUrl($"http://localhost:60981/chat?user={user}").Build();

            var e = this.connection.On("Chat", (string f, string m) => OnChatRecieved(f, m));

            await this.connection.StartAsync();
        }

        public async Task SendMessage(string message)
        {
            await this.connection.SendAsync("ChatAll", message);
        }

        public async Task<Dictionary<string, string>> GetUsers()
        {
            var users = await this.connection.InvokeAsync<Dictionary<string, string>>("GetUserData");

            return users;
        }
    }
}
