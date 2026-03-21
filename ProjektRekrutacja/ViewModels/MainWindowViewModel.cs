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
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var productsList = BuildQuery(context).ToList();

                Products.Clear();

                foreach (var product in productsList)
                {
                    Products.Add(product);
                }
            }
        }


        private IQueryable<Product> BuildQuery(AppDbContext context)
        {
            return
                from asortyment in context.Asortymenty

                where string.IsNullOrEmpty(SearchText)
                   || asortyment.Nazwa.Contains(SearchText)
                   || context.KodyKreskowe.Any(k =>
                        k.JednostkaMiaryAsortymentu_Id == asortyment.Id &&
                        k.Kod.Contains(SearchText))

                join waluta in context.Waluty
                    on asortyment.WalutaCenyEwidencyjnej_Id equals waluta.Id into walutyJoin
                from waluta in walutyJoin.DefaultIfEmpty()

                join stan in context.StanyMagazynowe
                    on asortyment.Id equals stan.Asortyment_Id into stanyJoin

                join jma in context.JednostkiMiarAsortymentow
                    on asortyment.Id equals jma.Asortyment_Id into jmaJoin
                from jma in jmaJoin.DefaultIfEmpty()

                join jednostka in context.JednostkiMiar
                    on jma.JednostkaMiary_Id equals jednostka.Id into jednostkiJoin
                from jednostka in jednostkiJoin.DefaultIfEmpty()

                join kod in context.KodyKreskowe
                    on asortyment.Id equals kod.JednostkaMiaryAsortymentu_Id into kodyJoin

                select new Product
                {
                    Name = asortyment.Nazwa,
                    Price = asortyment.CenaEwidencyjna ?? 0,
                    Currency = waluta != null ? waluta.Symbol : null,
                    Quantity = stanyJoin.Sum(s => (decimal?)s.IloscDostepna) ?? 0,
                    Unit = jednostka.Nazwa,
                    Barcode = kodyJoin.Select(k => k.Kod).FirstOrDefault()
                };
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