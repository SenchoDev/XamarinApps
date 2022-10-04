using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Bot.ViewModel.Helpers;
using Xamarin.Forms;

namespace Bot.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        BotServiceHelper botHelper;

        public Command SendCommand
        {
            get;
            set;
        }

        public ObservableCollection<ChatMessage> Messages
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public MainVM()
        {
            botHelper = new BotServiceHelper();
            SendCommand = new Command(SendActivity);
            Messages = new ObservableCollection<ChatMessage>();

            botHelper.MessageReceived += BotHelper_MessageReceived;
        }

        void SendActivity()
        {
            Messages.Add(new ChatMessage
            {
                Text = Message,
                IsIncoming = false
            });
            botHelper.SendActivity(Message);
            Message = string.Empty;
        }

        void BotHelper_MessageReceived(object sender, BotServiceHelper.BotResponseEventArgs e)
        {
            foreach(var activity in e.Activities)
            {
                if(activity.From.Id != "user1")
                {
                    Messages.Add(new ChatMessage
                    {
                        Text = activity.Text,
                        IsIncoming = true
                    });
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class ChatMessage
        {
            public string Id
            {
                get;
                set;
            }

            public string Text
            {
                get;
                set;
            }

            public bool IsIncoming
            {
                get;
                set;
            }
        }
    }
}
