using AdsAgregator.CommonModels.Enums;
using AdsAgregator.CommonModels.Models;
using FcmClient.Pages;
using FcmClient.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FcmClient
{
    public partial class App : Application
    {
        public static List<SearchItem> SearchListCache { get; set; } = new List<SearchItem>();
        public static List<AdModel> AdsListCache { get; set; } = new List<AdModel>();


        public App()
        {
            InitializeComponent();

            SubscribeForMessages();

          

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
            //MessagingCenter.Subscribe<NotificationCenter, string>(this, "SET_MOBILE_TOKEN_MESSAGE", SetMobileToken);
            MessagingCenter.Subscribe<NotificationCenter, Page>(this, "CHANGE_MAIN_PAGE", SetMainPage);
        }

        private void SetMainPage(NotificationCenter arg1, Page arg2)
        {
            MainPage = arg2;
        }

        private async void SetMobileToken(NotificationCenter arg1, string token)
        {
            ApplicationSettings.SetMobileToken(token);

            var userId = ApplicationSettings.GetUserId();

            // if user not signed in yet
            if (string.IsNullOrWhiteSpace(userId))
            {
                return;
            }

            var apiClient = new ApiClient.ApiClient();

            await  apiClient.SetMobileToken(userId, token);
            
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
