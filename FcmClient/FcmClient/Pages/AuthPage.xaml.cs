using FcmClient.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            SubscribeForMessages();
        }

        public void SubscribeForMessages()
        {
            MessagingCenter.Subscribe<NotificationCenter, ValueTuple<bool, string>>(this, "SIGNIN_RESULT_MESSAGE", OnSigninResultCallback);
        }

        private void OnSigninResultCallback(NotificationCenter obj, ValueTuple<bool, string> payload)
        {
            if (payload.Item1 == true)
            {
                var jObject = (JObject)JsonConvert.DeserializeObject(payload.Item2);

                var login = jObject.Property("userName")?.Value?.ToString();
                var password = jObject.Property("password")?.Value?.ToString();
                var token = jObject.Property("mobileAppToken")?.Value?.ToString();
                var userId = jObject.Property("id")?.Value?.ToString();
                ApplicationSettings.SetCredentials(userId,login, password, token);

                obj.ChangeMainPage(new MainPage());
            }
            else
            {
                DisplayAlert("", "Логін або пароль невірні", "ок");
            }
        }
    }
}