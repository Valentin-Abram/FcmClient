using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdsAgregator.CommonModels.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Telephony.Data;
using Android.Util;
using Android.Views;
using Android.Widget;
using FcmClient.Services;
using Firebase.Messaging;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FcmClient.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class AppFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            var notification = message.GetNotification();
            var data = message.Data["notificationPayload"];
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);

            if (!string.IsNullOrWhiteSpace(data))
            {
                var adList = new List<AdModel>();

                try
                {
                    adList = JsonConvert.DeserializeObject<List<AdModel>>(data);
                    new NotificationCenter().NewAdsArrived(adList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                
            }


        }
        


      
    }
}