using FcmClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FcmClient.ViewModels
{
    class AuthViewModel: INotifyPropertyChanged
    {
        private string _login;
        public string Login 
        {
            get 
            { 
                return _login; 
            }
            set 
            { 
                _login = value; 
                NotifyPropertyChanged();
            }
        }

        private string _password;
        public string Password 
        {
            get 
            { 
                return _password; 
            }
            set 
            { 
                _password = value; 
                NotifyPropertyChanged(); 
            }
        }

        public ICommand SignInCommand{ get; set; }


        public AuthViewModel()
        {
            RegisterCommands();
        }

        public void RegisterCommands()
        {
            SignInCommand = new Command(SignInCallback, CanSignIn);
        }

        private bool CanSignIn(object arg)
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        private async void SignInCallback(object obj)
        {
            await SignIn();
        }

        private async Task SignIn()
        {
            var token = ApplicationSettings.GetMobileToken();

            var client = new ApiClient.ApiClient();
            var signInResult = await client.SignInUser(Login, Password, token);


            new NotificationCenter().SendSinginResult(signInResult.Item1, signInResult.Item2);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
