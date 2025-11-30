using System.Windows;
using System.Windows.Input;
using NelayanGo.Helpers;

namespace NelayanGo.Views
{
    public partial class AnalisisWindow : Window
    {
        public AnalisisWindow()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        }

        private void LogOutLink_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var result = MessageBox.Show(
                "Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

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

    }
}
