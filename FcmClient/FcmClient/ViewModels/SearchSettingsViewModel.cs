using AdsAgregator.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FcmClient.ViewModels
{
    class SearchSettingsViewModel: INotifyPropertyChanged
    {
        public ICommand CreateCommand{ get; set; }
        public ICommand DeleteCommand{ get; set; }
        public ICommand EditCommand{ get; set; }
        public ICommand ChangeStatusCommand{ get; set; }

        public ObservableCollection<SearchItem> SearchItems { get; set; } = new ObservableCollection<SearchItem>();
       
        private SearchItem _selectedSearch;
        public SearchItem SelectedSearch 
        {
            get { return _selectedSearch; }
            set { _selectedSearch = value; NotifyPropertyChanged(); }
        }
        
        public SearchSettingsViewModel()
        {

        }

        private void InitCommands()
        {
            CreateCommand = new Command(Create);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            ChangeStatusCommand = new Command(ChangeStatus);
        }

        private async Task LoadSearchItems()
        { 
        
        }



        private void ChangeStatus(object obj)
        {
            throw new NotImplementedException();
        }

        private void Edit(object obj)
        {
            throw new NotImplementedException();
        }

        private void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        private void Create(object obj)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
