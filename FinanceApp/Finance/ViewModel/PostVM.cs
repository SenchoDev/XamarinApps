using System;
using System.ComponentModel;
using Finance.Interfaces;
using Finance.Model;
using Xamarin.Forms;

namespace Finance.ViewModel
{
    public class PostVM : INotifyPropertyChanged
    {
        private Item selectedPost;
        public Item SelectedPost
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                OnPropertyChanged("SelectedPost");
            }
        }

        public Command ShareCommand
        {
            get;
            set;
        }

        public PostVM()
        {
            SelectedPost = new Item();
            ShareCommand = new Command(Share);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Share()
        {
            IShare share = DependencyService.Get<IShare>();
            share.Show("Post from Finance", SelectedPost.ItemLink);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
