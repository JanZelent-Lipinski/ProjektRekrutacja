using ProjektRekrutacja.Models;
using ProjektRekrutacja.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ProjektRekrutacja.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public MainViewModel()
        {
            ClearSearchCommand = new RelayCommand(() =>
            {
                SearchText = string.Empty;
            });

            LoadProducts();
        }

        #endregion

        #region Properties

        
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                LoadProducts();
            }
        }

        
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        #endregion

        #region Commands

        
        public ICommand ClearSearchCommand { get; set; }

        #endregion

        #region Methods


        public void LoadProducts()
        {
            using (var context = new AppDbContext())
            {
                var productsList = context.Asortymenty

                    
                    .Where(asortyment =>
                        string.IsNullOrEmpty(SearchText) ||

                        
                        asortyment.Nazwa.Contains(SearchText) ||

                        
                        context.KodyKreskowe.Any(kodKreskowy =>
                            kodKreskowy.JednostkaMiaryAsortymentu_Id == asortyment.Id &&
                            kodKreskowy.Kod.Contains(SearchText)
                        )
                    )


                    .Select(asortyment => new Product
                    {
                        Name = asortyment.Nazwa,

                        Price = asortyment.CenaEwidencyjna ?? 0,

                        Currency = context.Waluty
                            .Where(waluta => waluta.Id == asortyment.WalutaCenyEwidencyjnej_Id)
                            .Select(waluta => waluta.Symbol)
                            .FirstOrDefault(),

                        Quantity = context.StanyMagazynowe
                            .Where(stanMagazynowy => stanMagazynowy.Asortyment_Id == asortyment.Id)
                            .Sum(stanMagazynowy => (decimal?)stanMagazynowy.IloscDostepna) ?? 0,

                        Unit = context.JednostkiMiarAsortymentow
                            .Where(jednostkaPowiazanie => jednostkaPowiazanie.Asortyment_Id == asortyment.Id)
                            .Join(context.JednostkiMiar,
                                jednostkaPowiazanie => jednostkaPowiazanie.JednostkaMiary_Id,
                                jednostka => jednostka.Id,
                                (jednostkaPowiazanie, jednostka) => jednostka.Nazwa)
                            .FirstOrDefault(),

                        Barcode = context.KodyKreskowe
                            .Where(kodKreskowy => kodKreskowy.JednostkaMiaryAsortymentu_Id == asortyment.Id)
                            .Select(kodKreskowy => kodKreskowy.Kod)
                            .FirstOrDefault()
                    })

                    .ToList();

                Products.Clear();

                foreach (var product in productsList)
                {
                    Products.Add(product);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}