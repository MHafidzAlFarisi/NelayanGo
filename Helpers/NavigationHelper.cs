using System.Windows;
using System.Windows.Input;
using System;
using NelayanGo.Views;
using System.Linq.Expressions;

namespace NelayanGo.Helpers
{
    /// <summary>
    /// Helper class statis untuk mengelola navigasi antar Window (jendela)
    /// dalam aplikasi WPF.
    /// </summary>
    public static class NavigationHelper
    {
        // ----------------------------------------------------
        // Metode Navigasi Publik
        // ----------------------------------------------------

        /// <summary>
        /// Membuka AnalisisWindow.
        /// </summary>
        /// <param name="currentWindow">Jendela saat ini yang memanggil navigasi (opsional, untuk menutupnya).</param>
        public static void NavigateKeHome(Window? currentWindow = null)
        {
            try
            {
                var analisisWindow = new AnalisisWindow();
                analisisWindow.Show();

                // Tutup jendela saat ini jika parameter currentWindow diberikan
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal membuka jendela Analisis: {ex.Message}", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void NavigateKeDaftarTangkapan(Window? currentWindow = null)
        {
            try
            {
                var daftarTangkapanWindow = new DaftarTangkapanWindow();
                daftarTangkapanWindow.Show();

                // Tutup jendela saat ini jika parameter currentWindow diberikan
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal membuka jendela Analisis: {ex.Message}", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void NavigateKePeta(Window? currentWindow = null)
        {
            try
            {
                var petaWindow = new PetaWindow();
                petaWindow.Show();

                // Tutup jendela saat ini jika parameter currentWindow diberikan
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal membuka jendela Analisis: {ex.Message}", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void NavigateKeHargaPasar(Window? currentWindow = null)
        {
            try
            {
                var hargaPasarWindow = new HargaPasarWindow();
                hargaPasarWindow.Show();

                // Tutup jendela saat ini jika parameter currentWindow diberikan
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal membuka jendela Analisis: {ex.Message}", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void NavigateKeLogin(Window? currentWindow = null)
        {
            try
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Gagal membuka jendelaLogin: {ex.Message}", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <param name="destinationName">Nama tujuan: "Home", "HasilTangkapan", "Maps", "HargaPasar".</param>
        public static void NavigateFromHeaderClick(object sender, string destinationName)
        {
            // Menemukan jendela induk dari objek yang diklik (sender)
            var element = sender as DependencyObject;
            Window? currentWindow = Window.GetWindow(element);

            switch (destinationName)
            {
                case "Home":
                    NavigateKeHome(currentWindow);
                    break;
                case "Daftar Tangkapan":
                    NavigateKeDaftarTangkapan(currentWindow);
                    break;
                case "Maps":
                    NavigateKePeta(currentWindow);
                    break;
                case "Harga pasar":
                    NavigateKeHargaPasar(currentWindow);
                    break;
                case "Logout":
                    NavigateKeLogin(currentWindow);
                    break;
                default:
                    MessageBox.Show("Tujuan navigasi tidak dikenal.", "Error Navigasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        /// <summary>
        /// Membuka DaftarTangkapanWindow (Hasil Tangkapan).
        /// </summary>

        // TODO: Tambahkan metode lain di sini (e.g., NavigateToMaps, NavigateToHargaPasar)
    }

    /// <summary>
    /// Kelas statis untuk digunakan di XAML (XAML Helper) yang mengikat event Mouse/Click 
    /// ke perintah statis yang memanggil metode navigasi.
    /// </summary>
    public static class WindowCommands
    {
        // Command untuk Navigasi Home (AnalisisWindow)
        public static readonly RoutedUICommand NavigateHome = new RoutedUICommand(
            "NavigateHome", "NavigateHome", typeof(WindowCommands));

        // Command untuk Navigasi Hasil Tangkapan
        public static readonly RoutedUICommand NavigateCatchList = new RoutedUICommand(
            "NavigateCatchList", "NavigateCatchList", typeof(WindowCommands));
    }
}