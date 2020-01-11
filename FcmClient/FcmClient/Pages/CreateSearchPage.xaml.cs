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
    public partial class CreateSearchPage : ContentPage
    {
        public CreateSearchPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<CreateSearchViewModel, bool>(this, "SEARCH_CREATION_RESULT", SearchCreationResultCallback);
        }

        private async void SearchCreationResultCallback(CreateSearchViewModel arg1, bool result)
        {
            if (result == true)
                await Navigation.PopModalAsync();
            else
                DisplayAlert("", "Невдалось створити пошук", "ок");
        }
    }
}