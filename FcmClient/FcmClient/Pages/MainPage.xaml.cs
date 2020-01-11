using ApiClient;
using FcmClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FcmClient.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetDataFromServer();

            new NotificationCenter().RequestMobileTokenRefresh();
        }

        private async Task GetDataFromServer()
        {
            //var client = new ApiClient.ApiClient();

            //var data = await client.GetSearches("2");

            //var newlyCreated = await client.UpdateSearch("3", "test title (edited)", "test description (edited)", "some fake url (edited)", false, 1, "2");

            //Created.Text = newlyCreated;

            //Message.Text = data;
        }
    }
}
