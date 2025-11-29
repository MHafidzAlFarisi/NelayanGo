using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using NelayanGo.DataServices;
using Supabase.Gotrue; // Pastikan ini ada

namespace NelayanGo.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _statusMessage;
        private bool _isLoggingIn;
        private string _fullName;
        public string FullName { get => _fullName; set { _fullName = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string StatusMessage { get => _statusMessage; set { _statusMessage = value; OnPropertyChanged(); } }
        public bool IsLoggingIn { get => _isLoggingIn; set { _isLoggingIn = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; }
        public ICommand GoogleLoginCommand { get; }

        public LoginViewModel()
        {
            // LoginCommand menggunakan async lambda
            LoginCommand = new RelayCommand(async (o) => await ExecuteLogin(), (o) => CanExecuteLogin());

            // GoogleLoginCommand memanggil method async void
            GoogleLoginCommand = new RelayCommand((o) => ExecuteGoogleLogin());
        }

        private bool CanExecuteLogin() => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !IsLoggingIn;

        private async Task ExecuteLogin()
        {
            IsLoggingIn = true;
            StatusMessage = "Sedang memproses login...";
            try
            {
                // Gunakan SupabaseConfig.Client
                var session = await SupabaseConfig.Client.Auth.SignIn(Email, Password);

                if (session?.User != null)
                {
                    StatusMessage = "Login Berhasil!";
                    OnLoginSuccess?.Invoke();
                }
                else
                {
                    StatusMessage = "Login gagal.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        // PERBAIKAN UTAMA: Tambahkan 'async' di sini
        private async void ExecuteGoogleLogin()
        {
            try
            {
                IsLoggingIn = true;
                StatusMessage = "Membuka browser...";

                var signInOptions = new SignInOptions
                {
                    RedirectTo = "nelayango://auth/callback"
                };

                // Sekarang 'await' valid karena method ini 'async'
                var authState = await SupabaseConfig.Client.Auth.SignIn(Supabase.Gotrue.Constants.Provider.Google, signInOptions);

                string url = authState.Uri.ToString();

                // Buka browser
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });

                StatusMessage = "Silakan login di browser.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Gagal membuka browser: {ex.Message}";
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        public event Action OnLoginSuccess;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Helper class untuk Command
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}