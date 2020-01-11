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
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateSearchPage()));
        }
    }
}