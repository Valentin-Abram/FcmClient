using AdsAgregator.CommonModels.Enums;
using AdsAgregator.CommonModels.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FcmClient.ViewModels
{
    class CreateSearchViewModel: INotifyPropertyChanged
    {
        private string _title;
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(); } }
        
        private string _description;
        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged(); } }

        private string _url;
        public string Url { get { return _url; } set { _url = value; NotifyPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }

        public CreateSearchViewModel()
        {
            SaveCommand = new Command(SaveSearch);
        }

        private async void SaveSearch(object obj)
        {
            

            var apiClient = new ApiClient.ApiClient();
            var result = await apiClient
                .CreateSearch(Title, Description, Url, default(bool), (int) AdSource.Ebay, ApplicationSettings.GetUserId() );

            try
            {
                var searchItem = JsonConvert.DeserializeObject<SearchItem>(result);

                if (searchItem?.Id > 0)
                {
                    MessagingCenter.Send<CreateSearchViewModel, bool>(this, "SEARCH_CREATION_RESULT", true);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<CreateSearchViewModel, bool>(this, "SEARCH_CREATION_RESULT", false);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
