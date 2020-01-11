using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FcmClient.Services
{
    public static class InternalCache
    {
        public static List<SearchItem> GetSearchList()
        {
            return App.SearchListCache;
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
