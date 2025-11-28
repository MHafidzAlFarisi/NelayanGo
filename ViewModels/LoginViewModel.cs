using NelayanGo.DataServices;
using NelayanGo.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NelayanGo.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email = string.Empty;
        private string _password = string.Empty;
        private readonly AuthService _authService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public ICommand LoginCommand { get; }

        // event untuk memberi tahu View kalau login sukses
        public event Action<UserAccount>? LoginSucceeded;

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new RelayCommand(DoLogin, CanLogin);
        }

        private bool CanLogin(object? p)
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        private void DoLogin(object? p)
        {
            var user = _authService.Login(Email, Password);

            if (user == null)
            {
                MessageBox.Show("Email atau password salah.", "Login gagal",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // kirim user ke View, biar View yang buka window sesuai role
            LoginSucceeded?.Invoke(user);
        }
    }
}
