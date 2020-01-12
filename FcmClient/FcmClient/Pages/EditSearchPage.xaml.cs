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
    public partial class EditSearchPage : ContentPage
    {
        public EditSearchPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<EditSearchViewModel, bool>(this, "SEARCH_EDIT_RESULT", SearchEditResultCallback);
        }

        private async void SearchEditResultCallback(EditSearchViewModel arg1, bool result)
        {
            if (result == true)
                await Navigation.PopModalAsync();
            else
                DisplayAlert("", "Невдалось відредагувати пошук", "ок");
        }
    }
}