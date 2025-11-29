using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NelayanGo.ViewModels;

namespace NelayanGo.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            // Set DataContext ke ViewModel baru
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;

            // Dengarkan event sukses dari ViewModel
            _viewModel.OnLoginSuccess += NavigateToMain;
        }

        // Dipanggil ketika tombol Login diklik (dari XAML)
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Ambil password dari PasswordBox dan masukkan ke ViewModel
            _viewModel.Password = PasswordBoxControl.Password;

            // 2. Jalankan Command Login di ViewModel secara manual
            if (_viewModel.LoginCommand.CanExecute(null))
            {
                _viewModel.LoginCommand.Execute(null);
            }
        }

        // Navigasi ke Halaman Utama
        private void NavigateToMain()
        {
            // Pastikan dijalankan di thread UI utama
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            });
        }

        // Handler untuk Google Sign In (Sekarang ditangani ViewModel via Command di XAML)
        // Namun jika tombol Anda di XAML menggunakan Click="...", ubah menjadi Command="{Binding GoogleLoginCommand}"
        // Atau panggil manual seperti ini:
        private void GoogleSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.GoogleLoginCommand.CanExecute(null))
            {
                _viewModel.GoogleLoginCommand.Execute(null);
            }
        }

        private void DaftarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Fitur pendaftaran belum diimplementasikan.");
            // new RegisterWindow().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}