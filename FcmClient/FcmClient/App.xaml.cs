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
            MessagingCenter.Subscribe<NotificationCenter, Page>(this, "SET_MOBILE_TOKEN_MESSAGE", SetMainPage);
        }

        private void SetMainPage(NotificationCenter arg1, Page arg2)
        {
            MainPage = arg2;
        }

        private void SetMobileToken(NotificationCenter arg1, string token)
        {
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
