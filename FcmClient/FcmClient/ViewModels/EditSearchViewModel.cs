using AdsAgregator.CommonModels.Enums;
using AdsAgregator.CommonModels.Models;
using FcmClient.Services;
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
    class EditSearchViewModel: INotifyPropertyChanged
    {
        private SearchItem _searchItem;

        private string _title;
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(); } }

        private string _description;
        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged(); } }

        private string _url;
        public string Url { get { return _url; } set { _url = value; NotifyPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }

        public EditSearchViewModel()
        {
            SaveCommand = new Command(SaveSearch);
        }

        public EditSearchViewModel SetEditingItem(SearchItem searchItem)
        {
            this._searchItem = searchItem;

            Title = this._searchItem.Title;
            Description = this._searchItem.Description;
            Url = this._searchItem.Url;

            return this;
        }

        private async void SaveSearch(object obj)
        {

            var apiClient = new ApiClient.ApiClient();
            var result = await apiClient
                .UpdateSearch(_searchItem.Id.ToString(), Title, Description, Url, _searchItem.IsActive, (int)AdSource.Ebay, ApplicationSettings.GetUserId());

            try
            {
                var searchItem = JsonConvert.DeserializeObject<SearchItem>(result);

                if (searchItem?.Id > 0)
                {
                    MessagingCenter.Send<EditSearchViewModel, bool>(this, "SEARCH_EDIT_RESULT", true);
                    InternalCache.UpdateSearchItem(searchItem);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<EditSearchViewModel, bool>(this, "SEARCH_EDIT_RESULT", false);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
