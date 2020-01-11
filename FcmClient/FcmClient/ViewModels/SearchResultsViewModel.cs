using AdsAgregator.CommonModels.Models;
using FcmClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FcmClient.ViewModels
{
    class SearchResultsViewModel : INotifyPropertyChanged
    {
        private const int ADS_LIST_MAX_SIZE = 100;
        public ObservableCollection<AdModel> Ads { get; set; } = new ObservableCollection<AdModel>();

        public SearchResultsViewModel()
        {
            SubscribeForMessages();
            InitCommands();
        }

        public ICommand VisitAdCommand { get; set; }

        public void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<NotificationCenter, List<AdModel>>(this, "NEW_ADS_ARRIVED_MESSAGE", OnNewAdsArrived);
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

        private void OnNewAdsArrived(NotificationCenter arg1, List<AdModel> arg2)
        {
            foreach (var item in arg2)
            {
                if (Ads.Count >= ADS_LIST_MAX_SIZE)
                    Ads.RemoveAt(Ads.Count - 1);

                Ads.Insert(0, item);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
