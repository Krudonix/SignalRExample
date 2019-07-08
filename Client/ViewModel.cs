using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client
{
    public class ViewModel
    {
        public ObservableCollection<ChatItem> ChatLog { get; } = new ObservableCollection<ChatItem>();

        public ICommand SendCommand { get; }

        public ICommand ConnectCommand { get; }

        public string User { get; set; }

        public string Message { get; set; }

        public ViewModel()
        {
            this.SendCommand = new Command(OnSendMessage);
            this.ConnectCommand = new Command(OnConnect);

            SignalRClient.Current.ChatRecieved += Current_ChatRecieved;           
        }

        private void Current_ChatRecieved(object sender, ChatEventArgs e)
        {
            var chat = new ChatItem(e.User, e.Message);
            Application.Current.Dispatcher.BeginInvoke((Action)(() => 
            {
                this.ChatLog.Add(chat);
            }));
        }

        private async void OnConnect()
        {
            await SignalRClient.Current.Connect(this.User);

            MessageBox.Show($"{this.User} Connected.");
        }

        public async void OnSendMessage()
        {
            await SignalRClient.Current.SendMessage(this.Message);
        }
    }
}
