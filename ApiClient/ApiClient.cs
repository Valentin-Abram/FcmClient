using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            this._endpoint = @"https://adsagregatorbackend.azurewebsites.net/api/";
        }

        public async Task<string> GetSearches(string userId)
        {
            var httpClient = new HttpClient();

            var response = await httpClient
                .GetAsync($"{_endpoint}searchitems/get?userId={userId}");

            return await response.Content.ReadAsStringAsync();
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
        public async Task<string> DeleteSearch(string userId, string itemId)
        {
            var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>()
            {
                { "userId", userId },
                { "itemId", itemId},
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync($"{_endpoint}/searchitems/delete", encodedContent);

            return await response.Content.ReadAsStringAsync();

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

            var response = await httpClient
                .GetAsync($"{_endpoint}user/signin?username={username}&&password={password}&&mobileToken={mobileToken}");

            bool signInResult = response.StatusCode == HttpStatusCode.OK;

            var content = await response.Content.ReadAsStringAsync();

            return new ValueTuple<bool, string>(signInResult, content);

        }



    }
}
