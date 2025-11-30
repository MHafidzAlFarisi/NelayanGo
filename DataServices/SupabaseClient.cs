using Supabase;
using System;
using NelayanGo.Utils;

namespace NelayanGo.DataServices
{
    public static class SupabaseConfig
    {
        public static string Url { get; private set; } = "";
        public static string Key { get; private set; } = "";

        private static Supabase.Client? _client;

        // Static constructor → dipanggil sekali SEBELUM class ini digunakan
        static SupabaseConfig()
        {
            EnvLoader.Load();

            Url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? "";
            Key = Environment.GetEnvironmentVariable("SUPABASE_KEY") ?? "";

            if (string.IsNullOrWhiteSpace(Url))
                throw new InvalidOperationException("SUPABASE_URL kosong atau tidak ditemukan di .env");

            if (!Url.StartsWith("https://"))
                throw new InvalidOperationException($"SUPABASE_URL harus https://..., sekarang: {Url}");

            if (string.IsNullOrWhiteSpace(Key))
                throw new InvalidOperationException("SUPABASE_KEY kosong atau tidak ditemukan di .env");
        }

        public static Supabase.Client Client
        {
            get
            {
                if (_client == null)
                {
                    var options = new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                    };

                    _client = new Supabase.Client(Url, Key, options);
                }

                return _client;
            }
        }
    }
}
