using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ApiClient;
using FcmClient.Services;
using System.Linq;
using Newtonsoft.Json;
using FcmClient.DisplayModels;
using Xamarin.Forms.Internals;

namespace FcmClient.ViewModels
{
    class SearchSettingsViewModel: INotifyPropertyChanged
    {
        private ApiClient.ApiClient _apiClient = new ApiClient.ApiClient();
        public ICommand CreateCommand{ get; set; }
        public ICommand DeleteCommand{ get; set; }
        public ICommand EditCommand{ get; set; }
        public ICommand ChangeStatusCommand{ get; set; }

        public ObservableCollection<SearchItemDisplayModel> SearchItems { get; set; } = new ObservableCollection<SearchItemDisplayModel>();
       
        private SearchItem _selectedSearch;
        public SearchItem SelectedSearch 
        {
            get { return _selectedSearch; }
            set { _selectedSearch = value; NotifyPropertyChanged(); }
        }
        
        public SearchSettingsViewModel()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            ChangeStatusCommand = new Command(ChangeStatus);
        }

        public async Task LoadSearchItems()
        {
            SearchItems.Clear();
            var itemList = InternalCache.GetSearchList();

            if (itemList?.Count > 0)
            {
                foreach (var item in itemList)
                {
                    SearchItems.Add(new SearchItemDisplayModel(item));
                }
            }
            else
            {
                itemList = await _apiClient.GetSearches(ApplicationSettings.GetUserId());
                
                foreach (var item in itemList)
                {
                    SearchItems.Add(new SearchItemDisplayModel(item));
                    InternalCache.AddSearchItem(item);
                }
            }

            SearchItems.ForEach(i => i.PropertyChanged += ItemPropertyChanged);
                
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.ToUpper() == "ISACTIVE")
            {
                ChangeStatus(sender);
            }
        }

        private async void ChangeStatus(object obj)
        {
            var searchItem = obj as SearchItem;

            var result = await _apiClient
                .UpdateSearch(
                searchItem.Id.ToString(),
                searchItem.Title,
                searchItem.Description,
                searchItem.Url,
                searchItem.IsActive,
                (int)searchItem.AdSource,
                ApplicationSettings.GetUserId());

            try
            {
                searchItem = JsonConvert.DeserializeObject<SearchItem>(result);

                if (searchItem?.Id > 0)
                {
                    //var index = SearchItems.IndexOf(SearchItems.FirstOrDefault(si => si.Id == searchItem.Id));

                    //if (index < 0)
                    //    throw new Exception();

                    //SearchItems.RemoveAt(index);
                    InternalCache.UpdateSearchItem(searchItem);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<SearchSettingsViewModel, bool>(this, "SEARCH_EDIT_RESULT", false);
            }
        }

        private void Edit(object obj)
        {
            MessagingCenter.Send<SearchSettingsViewModel, SearchItem>(this, "SEARCH_EDIT_MESSAGE", obj as SearchItem);
        }

        private async void Delete(object obj)
        {
            var searchItem = obj as SearchItem;

            var result = await _apiClient.DeleteSearch(ApplicationSettings.GetUserId(), searchItem.Id.ToString());

            if (result == System.Net.HttpStatusCode.OK)
            { 
                InternalCache.RemoveSearchItem(searchItem);
                SearchItems.Remove(SearchItems.FirstOrDefault(si => si.Id == searchItem.Id));
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
