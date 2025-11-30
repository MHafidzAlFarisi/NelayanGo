using System;
using System.Windows;
using NelayanGo.DataServices;

namespace NelayanGo.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthService _auth = new();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim() ?? "";
            var email = EmailTextBox.Text?.Trim() ?? "";
            var password = PasswordBox.Password ?? "";
            var confirmPassword = ConfirmPasswordBox.Password ?? "";

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username, email, dan password wajib diisi.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Konfirmasi password tidak sama.");
                return;
            }

            try
            {
                var user = _auth.Register(username, email, password, "nelayan");

                if (user == null)
                {
                    MessageBox.Show("Username atau email sudah digunakan.");
                    return;
                }

                MessageBox.Show("Registrasi berhasil. Silakan login.");
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat registrasi:\n{ex.Message}");
            }
        }

        private void LoginLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
