namespace Client
{
    public class ChatItem
    {
        public string User { get; }

        public string Message { get; }

        public ChatItem(string user, string message)
        {
            this.User = user;
            this.Message = message;
        }
    }
}
