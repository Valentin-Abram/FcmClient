using AdsAgregator.CommonModels.Models;
using FcmClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FcmClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchSettingsPage : ContentPage
    {
        public SearchSettingsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<SearchSettingsViewModel, bool>(this, "SEARCH_EDIT_RESULT", OnSearchEditResult);
            MessagingCenter.Subscribe<SearchSettingsViewModel, SearchItem>(this, "SEARCH_EDIT_MESSAGE", OnSearchEdit);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (this.BindingContext as SearchSettingsViewModel).LoadSearchItems();
        }

        private async void OnSearchEdit(SearchSettingsViewModel arg1, SearchItem arg2)
        {
            await Navigation.PushModalAsync(
                new NavigationPage(
                    new EditSearchPage() 
                    { 
                        BindingContext = new EditSearchViewModel()
                        .SetEditingItem(arg2)
                    })
                );
        }

        private void OnSearchEditResult(SearchSettingsViewModel obj, bool isSucceeded)
        {
            if (!isSucceeded)
            {
                DisplayAlert("", "Невдалось відредагувати", "ок");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateSearchPage()));
        }
    }
}