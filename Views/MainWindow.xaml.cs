using NelayanGo.Helpers;
using NelayanGo.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NelayanGo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var inputWindow = new InputTangkapanWindow();
            //inputWindow.Show();
            DataContext = new ViewModelsNelayan();
        }
        private void HomeLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Panggil NavigationHelper untuk membuka AnalisisWindow
            NavigationHelper.NavigateKeHome(this); // Pass 'this' jika Anda ingin MainWindow ditutup
        }

        private void LogoutLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateKeLogin(this);
        }
    }
}