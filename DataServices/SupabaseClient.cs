using Supabase;
using System;
using DotNetEnv;

namespace NelayanGo.DataServices
{
    // PERBAIKAN: Bungkus logika dalam public static class
    public static class SupabaseConfig
    {
        // PERBAIKAN: Deklarasikan variabel _client
        private static Supabase.Client? _client;

        public static Supabase.Client Client
        {
            get
            {
                if (_client == null)
                {
                    // Memuat variabel dari file .env
                    Env.Load();

                    var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
                    var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

                    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
                    {
                        throw new InvalidOperationException("Supabase URL atau Key tidak ditemukan di .env");
                    }

                    var options = new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        // PersistSession dihapus sementara karena menyebabkan error di beberapa versi SDK.
                        // Sesi akan ditangani manual di App.xaml.cs untuk OAuth.
                    };

                    _client = new Supabase.Client(url, key, options);
                }
                return _client;
            }
        }
    }
}