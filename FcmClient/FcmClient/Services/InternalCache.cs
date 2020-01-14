using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FcmClient.Services
{
    public static class InternalCache
    {
        private const int AD_LIST_CACHE_SIZE = 100;
        public static List<SearchItem> GetSearchList()
        {
            return App.SearchListCache;
        }

        public static void InsertAd(int index, AdModel ad)
        {
            if (App.AdsListCache.Count >= AD_LIST_CACHE_SIZE)
                App.AdsListCache.RemoveAt(AD_LIST_CACHE_SIZE - 1);

            App.AdsListCache.Insert(index, ad);
        }

        public static void AddSearchItem(SearchItem searchItem)
        {
            App.SearchListCache.Add(searchItem);
        }

        public static void UpdateSearchItem(SearchItem searchItem)
        {
           
            var item = App.SearchListCache
                .FirstOrDefault(si => si.Id == searchItem.Id);

            if (item is null)
                return;

            var index = App.SearchListCache.IndexOf(item);
            
            App.SearchListCache.Remove(item);
            App.SearchListCache.Insert(index, searchItem);
        }

        public static void RemoveSearchItem(SearchItem searchItem)
        {
            var item = App.SearchListCache
               .FirstOrDefault(si => si.Id == searchItem.Id);

            if (item is null)
                return;

            App.SearchListCache.Remove(item);
        }
    }
}
