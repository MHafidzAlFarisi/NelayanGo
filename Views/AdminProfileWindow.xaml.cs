using NelayanGo.Helpers;
using NelayanGo.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NelayanGo.Views
{
    /// <summary>
    /// Interaction logic for AdminProfileWindow.xaml
    /// </summary>
    public partial class AdminProfileWindow : Window
    {
        public AdminProfileWindow()
        {
            InitializeComponent();

            // Ensure the same ViewModel instance that XAML expects is used
            DataContext = new AdminProfileViewModel();
        }

        private void HargaIkanLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Harga Ikan");
        }

        private void LogOutLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show(
                "Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            AppSession.CurrentUser = null;

            var loginWindow = new LoginWindow();
            loginWindow.Show();

            Application.Current.Windows
                .OfType<Window>()
                .Where(w => w != loginWindow)
                .ToList()
                .ForEach(w => w.Close());
        }

        private void ProfileHeader_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Admin Profil");
        }
    }
}