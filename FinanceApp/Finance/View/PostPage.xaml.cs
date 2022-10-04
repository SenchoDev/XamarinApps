using System;
using System.Collections.Generic;
using Finance.Model;
using Finance.ViewModel;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Finance.View
{
    public partial class PostPage : ContentPage
    {
        PostVM ViewModel;
        public PostPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            ViewModel = Resources["vm"] as PostVM;
        }

        public PostPage(Item item)
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            ViewModel = Resources["vm"] as PostVM;
            ViewModel.SelectedPost = item;

            try
            {
                Title = item.Title;
                postImage.Source = item.Enclosure.Url;
                creatorLabel.Text = item.Creator;
                dateLabel.Text = item.PublishedDate.ToString("MMMM dd");
                descriptionLabel.Text = item.Description;

                var properties = new Dictionary<string, string>
                {
                    {"Blog_Post", $"{item.Title}"}
                };

                TrackEvent(properties);
            }
            catch(Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    {"Blog_Post", $"{item.Title}"}
                };
                TrackError(ex, properties);
            }
        }

        private async void TrackEvent(Dictionary<string, string> properties)
        {
            if (await Analytics.IsEnabledAsync())
                Analytics.TrackEvent("Blog_Post_Opened", properties);
        }

        private async void TrackError(Exception ex, Dictionary<string, string> properties)
        {
            if (await Crashes.IsEnabledAsync())
                Crashes.TrackError(ex, properties);
        }
    }
}
