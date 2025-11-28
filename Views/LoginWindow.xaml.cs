using NelayanGo.Models;
using NelayanGo.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace NelayanGo.Views
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel Vm => DataContext as LoginViewModel;

        public LoginWindow()
        {
            InitializeComponent();

            var vm = new LoginViewModel();
            vm.LoginSucceeded += OnLoginSucceeded;
            DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Vm != null)
            {
                Vm.Password = PasswordBox.Password;
            }
        }

        private void OnLoginSucceeded(UserAccount user)
        {
            // cek role
            if (user.Role == "Admin")
            {
                var adminWin = new AdminMainWindow();    // TODO: buat window ini
                adminWin.Show();
            }
            else // Petani / User biasa
            {
                var petaniWin = new PetaniMainWindow(user); // TODO: buat window ini
                petaniWin.Show();
            }

            this.Close(); // tutup window login
        }
    }
}
