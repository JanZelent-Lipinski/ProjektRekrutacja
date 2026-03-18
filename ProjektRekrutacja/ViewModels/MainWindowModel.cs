using ProjektRekrutacja.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


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
                LoadProducts();
            }
        }
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public void LoadProducts()
        {
            using (var context = new AppDbContext())
            {
                var productsQuery = context.Asortymenty

                    
                    .Where(product =>
                        string.IsNullOrEmpty(SearchText) ||
                        product.Nazwa.Contains(SearchText) ||
                        context.KodyKreskowe.Any(barcode =>
                            barcode.JednostkaMiaryAsortymentu_Id == product.Id &&
                            barcode.Kod.Contains(SearchText)
                        )
                    )

                    
                    .GroupJoin(context.StanyMagazynowe,
                        product => product.Id,
                        stock => stock.Asortyment_Id,
                        (product, stocks) => new { product, stocks })
                    .SelectMany(x => x.stocks.DefaultIfEmpty(),
                        (x, stock) => new { x.product, stock })

                    
                    .GroupJoin(context.JednostkiMiarAsortymentow,
                        x => x.product.Id,
                        unitLink => unitLink.Asortyment_Id,
                        (x, unitLinks) => new { x, unitLinks })
                    .SelectMany(x => x.unitLinks.DefaultIfEmpty(),
                        (x, unitLink) => new { x.x.product, x.x.stock, unitLink })

                    
                    .GroupJoin(context.KodyKreskowe,
                        x => x.unitLink.Asortyment_Id,
                        barcode => barcode.JednostkaMiaryAsortymentu_Id,
                        (x, barcodes) => new { x, barcodes })
                    .SelectMany(x => x.barcodes.DefaultIfEmpty(),
                        (x, barcode) => new { x.x.product, x.x.stock, x.x.unitLink, barcode })

                    
                    .Join(context.JednostkiMiar,
                        x => x.unitLink.JednostkaMiary_Id,
                        unit => unit.Id,
                        (x, unit) => new Product
                        {
                            Name = x.product.Nazwa,
                            Price = x.product.CenaEwidencyjna ?? 0,
                            Quantity = x.stock != null ? x.stock.IloscDostepna ?? 0 : 0,
                            Unit = unit.Nazwa,
                            Barcode = x.barcode != null ? x.barcode.Kod : null
                        })

                    .ToList();

                Products.Clear();

                foreach (var product in productsQuery)
                {
                    Products.Add(product);
                }
            }
        }
    }
}