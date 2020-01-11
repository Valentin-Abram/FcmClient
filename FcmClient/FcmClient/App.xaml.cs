using FcmClient.Pages;
using FcmClient.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FcmClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SubscribeForMessages();

            var token = ApplicationSettings.GetMobileToken();

            if (string.IsNullOrWhiteSpace(token))
                new NotificationCenter().RequestMobileTokenRefresh();

            if (IsAuthentificated() == true)
                MainPage = new MainPage();
            else
                MainPage = new AuthPage();
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<NotificationCenter, string>(this, "MOBILE_TOKEN_REFRESHED_MESSAGE", SetMobileToken);
            MessagingCenter.Subscribe<NotificationCenter, string>(this, "SET_MOBILE_TOKEN_MESSAGE", SetMobileToken);
            MessagingCenter.Subscribe<NotificationCenter, Page>(this, "CHANGE_MAIN_PAGE", SetMainPage);
        }

        private void SetMainPage(NotificationCenter arg1, Page arg2)
        {
            MainPage = arg2;
        }

        private async void SetMobileToken(NotificationCenter arg1, string token)
        {

            var credentials = ApplicationSettings.GetCredentials();
            var apiClient = new ApiClient.ApiClient();

            if (string.IsNullOrWhiteSpace(token))
                return;

            await  apiClient.SetMobileToken(credentials.Item1, credentials.Item2);
            ApplicationSettings.SetMobileToken(token);
        }

        private bool IsAuthentificated()
        {
            var credentials = ApplicationSettings.GetCredentials();
            if (string.IsNullOrWhiteSpace(credentials.Item1) | string.IsNullOrWhiteSpace(credentials.Item2))
                return false;

            return true;
        }
    }
}
