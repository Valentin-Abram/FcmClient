using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FcmClient.DisplayModels
{
    class SearchItemDisplayModel: SearchItem, INotifyPropertyChanged
    {
        public override bool IsActive 
        {
            get 
            {
                return base.IsActive; 
            }
            set 
            {
                base.IsActive = value;
                NotifyPropertyChanged();
            }
        }

        public SearchItemDisplayModel(SearchItem searchItem)
        {
            this.Id = searchItem.Id;
            this.Title = searchItem.Title;
            this.Description = searchItem.Description;
            this.Url = searchItem.Url;
            this.IsActive = searchItem.IsActive;
            this.AdSource = searchItem.AdSource;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
