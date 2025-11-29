using System.Windows;
using System.Windows.Input;
using NelayanGo.Views; // Asumsikan MainWindow ada di Views

namespace NelayanGo.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Tambahkan logika autentikasi di sini

            // Contoh: Setelah login berhasil, tampilkan MainWindow dan tutup LoginWindow
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;

            // Jika autentikasi berhasil (logika sederhana untuk demonstrasi)
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Tutup jendela login
            }
            else
            {
                MessageBox.Show("Email atau Password tidak boleh kosong.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                var client = SupabaseClient.Client;

                var session = await client.Auth.SignIn(email, password);

                if (session != null && session.User != null)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Login gagal.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}");
            }
        }

        private void GoogleSignIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Membuka proses Sign In dengan Google...", "Info");
            // TODO: Implementasi integrasi Google Sign-In
        }

        private void DaftarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Membuka halaman Pendaftaran...", "Info");
            // TODO: Buka jendela Pendaftaran (misalnya, new DaftarWindow().Show())
        }
    }
}