using NelayanGo.Helpers;
using System.Windows;
using System.Windows.Input;

namespace NelayanGo.Views
{
    public partial class HargaPasarWindow : Window
    {
        public HargaPasarWindow()
        {
            InitializeComponent();
        }

        private void HomeLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Home");
        }

        private void HasilTangkapanLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        }

        private void MapsLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Maps");
        }
        private void HargaPasarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Harga pasar");
        }
        private void ProfileHeader_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Profil");
        }
    }
}