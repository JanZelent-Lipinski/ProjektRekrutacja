using System.ComponentModel;
using System.Collections.ObjectModel;
using ProjektRekrutacja.Models;

namespace ProjektRekrutacja.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    }
}