using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FcmClient.Services
{
    public class NotificationCenter
    {
        public void NewAdsArrived(List<AdModel> ads)
        {
            MessagingCenter.Send(this, "NEW_ADS_ARRIVED_MESSAGE", ads);
        }
        public void RequestMobileTokenRefresh()
        { 
            MessagingCenter.Send(this, "REFRESH_MOBILE_TOKEN_MESSAGE");
        }
        public void MobileTokenRefreshed(string token)
        { 
            MessagingCenter.Send(this, "MOBILE_TOKEN_REFRESHED_MESSAGE", token);
        }
        public void SetMobileToken(string token)
        {
            MessagingCenter.Send(this, "SET_MOBILE_TOKEN_MESSAGE", token);
        }
        public void SendSinginResult(bool isSucceeded, string payload)
        { 
            MessagingCenter.Send(this, "SIGNIN_RESULT_MESSAGE", new ValueTuple<bool,string>(isSucceeded, payload));
        }
        public void ChangeMainPage(Page page)
        {
            MessagingCenter.Send(this, "CHANGE_MAIN_PAGE", page);
        }

    }
}
