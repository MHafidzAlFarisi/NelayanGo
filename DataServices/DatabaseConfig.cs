using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.Utils;

namespace NelayanGo.DataServices
{
    public static class DatabaseConfig
    {
        public static string ConnectionString
        {
            get
            {
                EnvLoader.Load();   // pastikan .env sudah dibaca

                var host = Environment.GetEnvironmentVariable("SUPABASE_HOST")
                           ?? "";
                var port = Environment.GetEnvironmentVariable("SUPABASE_PORT") ?? "";
                var user = Environment.GetEnvironmentVariable("SUPABASE_USER")
                           ?? "";
                var password = Environment.GetEnvironmentVariable("SUPABASE_PASSWORD") ?? "";
                var db = Environment.GetEnvironmentVariable("SUPABASE_DB") ?? "";

                return $"Host={host};Port={port};Username={user};Password={password};" +
                       $"Database={db};Ssl Mode=Require;Trust Server Certificate=true;";
            }
        }
    }
}

