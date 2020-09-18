using AdsAgregator.CommonModels.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ApiClient
    {
        private string _endpoint { get; set; }

        public ApiClient()
        {
            this._endpoint = @"https://adsagregator2020.azurewebsites.net/api/";
        }

        public async Task<List<AdModel>> GetAds(int userId, int adIdFrom)
        {
            var httpClient = new HttpClient();

            var response = await httpClient
                .GetAsync($"{_endpoint}Ads/GetAds?userId={userId}&adIdFrom={adIdFrom}");

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
                return new List<AdModel>();

            var searchList = new List<AdModel>();

            try
            {
                searchList = JsonConvert
                    .DeserializeObject<List<AdModel>>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return searchList;
        }

        public async Task<List<SearchItem>> GetSearches(string userId)
        {
            var httpClient = new HttpClient();

            var response = await httpClient
                .GetAsync($"{_endpoint}searchitems/get?userId={userId}");

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
                return null;

            var searchList = JsonConvert
                .DeserializeObject<List<SearchItem>>(content);

            return searchList;
        }

        public async Task<string> CreateSearch(string title, string description, string url, bool isActive, int adSource, string userId)
        {
            var searchItem = new 
            { 
                Title = title,
                Description = description,
                Url = url,
                IsActive = isActive,
                AdSource = adSource,
                OwnerId = userId
            };

            var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>()
            {
                { "userId", userId.ToString() },
                { "value", JsonConvert.SerializeObject(searchItem)},
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync($"{_endpoint}/searchitems/create", encodedContent);

            return await response.Content.ReadAsStringAsync();

        }  

        public async Task<string> UpdateSearch(string id, string title, string description, string url, bool isActive, int adSource, string userId)
        {
            var searchItem = new 
            { 
                Id = id,
                Title = title,
                Description = description,
                Url = url,
                IsActive = isActive,
                AdSource = adSource,
                OwnerId = userId
            };

            var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>()
            {
                { "userId", userId.ToString() },
                { "value", JsonConvert.SerializeObject(searchItem)},
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync($"{_endpoint}/searchitems/update", encodedContent);

            return await response.Content.ReadAsStringAsync();

        }



        // TODO : test api call       
        public async Task<HttpStatusCode> DeleteSearch(string userId, string itemId)
        {
            var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>()
            {
                { "userId", userId },
                { "itemId", itemId},
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync($"{_endpoint}/searchitems/delete", encodedContent);

            return response.StatusCode;

        }
        public async Task<string> RegisterUser(string username, string password, string mobileToken)
        {

            var httpClient = new HttpClient();

            var response = await httpClient
                .GetAsync($"{_endpoint}user/register?username={username}&&password={password}&&mobileToken={mobileToken}");

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<ValueTuple<bool,string>> SignInUser(string username, string password, string mobileToken)
        {

            var httpClient = new HttpClient();

            var url = $"{_endpoint}user/signin?username={username}&password={password}&mobileToken={mobileToken}";

            var response = await httpClient
                .GetAsync(url);

            bool signInResult = response.StatusCode == HttpStatusCode.OK;

            var content = await response.Content.ReadAsStringAsync();

            return new ValueTuple<bool, string>(signInResult, content);

        }


        public async Task<ValueTuple<bool, string>> SetMobileToken(string userId, string mobileToken)
        {

            var httpClient = new HttpClient();

            var url = $"{_endpoint}user/SetMobileToken?userId={userId}&mobileToken={mobileToken}";

            var response = await httpClient
                .GetAsync(url);

            bool result = response.StatusCode == HttpStatusCode.OK;

            var content = await response.Content.ReadAsStringAsync();

            return new ValueTuple<bool, string>(result, content);

        }



    }
}
