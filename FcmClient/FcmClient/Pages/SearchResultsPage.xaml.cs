using AdsAgregator.CommonModels.Models;
using FcmClient.Services;
using FcmClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FcmClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsPage : ContentPage
    {
        public SearchResultsPage()
        {
            InitializeComponent();
            SubscribeForMessages();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            new NotificationCenter().NewAdsArrived();

        }

        private void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<SearchResultsViewModel, string>(this, "VISIT_AD_MESSAGE", VisitAd);
        }

        private async void VisitAd(SearchResultsViewModel sender, string link)
        {
            var url = link.Replace(" ", "");

            await Launcher.OpenAsync(new Uri(url, true));
        }




    }
}