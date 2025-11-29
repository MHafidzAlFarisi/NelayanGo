// ... namespace lain ...
using NelayanGo.DataServices;
using NelayanGo.Views;
using System.Windows;

namespace NelayanGo
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args != null && e.Args.Length > 0)
            {
                string url = e.Args[0];
                if (url.StartsWith("nelayango://auth/callback"))
                {
                    try
                    {
                        // ... (Kode parsing URL tetap sama) ...
                        // Ganti '#' dengan '?' untuk parsing query string
                        var query = url.Contains("#") ? url.Substring(url.IndexOf("#") + 1) : new Uri(url).Query;
                        var queryParams = System.Web.HttpUtility.ParseQueryString(query); // Butuh referensi System.Web
                        var accessToken = queryParams.Get("access_token");
                        var refreshToken = queryParams.Get("refresh_token");

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // PERBAIKAN: Gunakan SupabaseConfig.Client
                            await SupabaseConfig.Client.Auth.SetSession(accessToken, refreshToken);

                            var mainWindow = new MainWindow();
                            mainWindow.Show();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Login Error: {ex.Message}");
                    }
                }
            }
            // ... (Buka LoginWindow biasa) ...
            new LoginWindow().Show();
        }
    }
}