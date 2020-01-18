using AdsAgregator.CommonModels.Models;
using FcmClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FcmClient.ViewModels
{
    class SearchResultsViewModel : INotifyPropertyChanged
    {
        private const int ADS_LIST_MAX_SIZE = 100;
        private ApiClient.ApiClient _apiClient = new ApiClient.ApiClient();
        public ObservableCollection<AdModel> Ads { get; set; } = new ObservableCollection<AdModel>();


        public SearchResultsViewModel()
        {
            SubscribeForMessages();
            InitCommands();
        }

        public ICommand VisitAdCommand { get; set; }

        public void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<NotificationCenter>(this, "NEW_ADS_ARRIVED_MESSAGE", OnNewAdsArrived);
        }

        private void InitCommands()
        {
            VisitAdCommand = new Command(VisitAd);
        }

        private void VisitAd(object data)
        {
            var adLink = data as string;

            MessagingCenter.Send(this, "VISIT_AD_MESSAGE", adLink);
        }

        private async void OnNewAdsArrived(NotificationCenter arg1)
        {
            int adIdFrom = 0;

            if (Ads?.Count > 0)
                adIdFrom = Ads.Max(ad => ad.Id);


            var latestAds = await _apiClient.GetAds(Convert.ToInt32(ApplicationSettings.GetUserId()), adIdFrom);

            if (latestAds != null & latestAds.Count > 0)
            {
                foreach (var item in latestAds)
                {
                    if (Ads.Count >= ADS_LIST_MAX_SIZE)
                        Ads.RemoveAt(Ads.Count - 1);

                    Ads.Insert(0, item);
                    InternalCache.InsertAd(0,item);
                }
            }


        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
