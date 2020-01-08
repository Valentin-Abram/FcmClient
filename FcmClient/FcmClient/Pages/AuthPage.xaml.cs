using FcmClient.Services;
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
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        public void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<NotificationCenter, ValueTuple<bool, string>>(this, "SIGNIN_RESULT_MESSAGE", OnSigninResultCallback);
        }

        private void OnSigninResultCallback(NotificationCenter obj, ValueTuple<bool, string> payload)
        {
            if (payload.Item1 == true)
            { 
                
            }
        }
    }
}