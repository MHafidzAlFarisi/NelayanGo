using NelayanGo.Helpers;
using System.Windows;
using System.Windows.Input;


namespace NelayanGo.Views
{
    /// <summary>
    /// Interaction logic for AnalisisWindow.xaml
    /// </summary>
    public partial class AnalisisWindow : Window
    {

        public AnalisisWindow()
        {
            InitializeComponent();
        }

        private void HomeLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Cukup panggil helper, dan berikan tujuan sebagai string
            NavigationHelper.NavigateFromHeaderClick(sender, "Home");
        }

        private void HasilTangkapanLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Cukup panggil helper, dan berikan tujuan sebagai string
            NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        }

        // Tambahkan handler untuk navigasi lain
        private void MapsLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Maps");
        }
        // Tambahkan handler untuk navigasi lain
        private void HargaPasarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Harga pasar");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        }
        private void ProfileHeader_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Profil");
        }
    }

}