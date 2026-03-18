using System.Windows;
using ProjektRekrutacja.ViewModels;

namespace ProjektRekrutacja.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainViewModel();
            DataContext = vm;

            vm.LoadProducts();
        }
    }
}