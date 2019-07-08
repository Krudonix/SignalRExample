using System;

namespace Client
{
    public class ChatEventArgs : EventArgs
    {
        public string User { get; }
        public string Message { get; }

        public ChatEventArgs(string user, string message)
        {
            this.User = user;
            this.Message = message;
        }
    }
}
