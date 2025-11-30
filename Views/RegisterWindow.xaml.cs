using System;
using System.Windows;
using NelayanGo.DataServices;
using NelayanGo.Models;

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
                    MessageBox.Show("Gagal mendaftar. Username atau email mungkin sudah digunakan.");
                    return;
                }

                // JANGAN simpan sesi di sini agar user dipaksa login nanti
                // AppSession.CurrentUser = user; <--- HAPUS atau COMMENT baris ini jika ada

                MessageBox.Show("Registrasi berhasil! Silakan lengkapi data diri Anda sebelum Login.", "Sukses");

                // Buka Input Data
                var inputDataWindow = new InputDataNelayanWindow(user.Id);

                // PERBAIKAN: Gunakan ShowDialog() agar InputDataNelayanWindow bisa menggunakan 'DialogResult'
                // Jika menggunakan .Show(), baris 'DialogResult = true' di InputDataNelayanWindow akan error.
                inputDataWindow.ShowDialog();

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