using System;
using System.Windows;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _auth = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var identifier = LoginTextBox.Text?.Trim() ?? "";
            var password = PasswordBox.Password ?? "";

            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email/username dan password wajib diisi.");
                return;
            }

            var user = _auth.Login(identifier, password);

            if (user == null)
            {
                MessageBox.Show("Login gagal. Cek lagi email/username dan password.");
                return;
            }

            // (opsional) simpan user login di session global
            AppSession.CurrentUser = user;

            // Bedakan admin vs nelayan
            if (string.Equals(user.Role, "admin", StringComparison.OrdinalIgnoreCase))
            {
                var adminWin = new AdminHargaIkanWindow();   // atau AdminProfileWindow
                adminWin.Show();
            }
            else
            {
                var main = new AnalisisWindow();                 // dashboard nelayan
                main.Show();
            }

            this.Close();
        }

        private void RegisterLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var reg = new RegisterWindow();
            reg.Show();
            this.Close();
        }
    }
}
