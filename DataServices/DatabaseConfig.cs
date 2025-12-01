using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NelayanGo.DataServices
{
    public static class DatabaseConfig
    {
        // Runtime-resolved connection string (not const because resolved at startup)
        public static readonly string ConnectionString = InitializeConnectionString();

        private static string InitializeConnectionString()
        {
            // 1) Prefer an explicit connection string from environment
            var explicitKeys = new[] { "CONNECTION_STRING", "DATABASE_CONNECTION_STRING", "DB_CONNECTION_STRING" };
            foreach (var key in explicitKeys)
            {
                var val = GetEnvOrDotEnvValue(key);
                if (!string.IsNullOrWhiteSpace(val))
                    return val;
            }

            // 2) Otherwise build from SUPABASE_* variables
            var requiredKeys = new[] { "SUPABASE_HOST", "SUPABASE_USER", "SUPABASE_PASSWORD", "SUPABASE_DB" };
            var allKeys = requiredKeys.Concat(new[] { "SUPABASE_PORT", "SUPABASE_URL", "SUPABASE_KEY" }).ToArray();

            var values = allKeys.ToDictionary(k => k, k => GetEnvOrDotEnvValue(k), StringComparer.OrdinalIgnoreCase);

            var missing = requiredKeys.Where(k => string.IsNullOrWhiteSpace(values[k])).ToList();
            if (missing.Any())
            {
                throw new InvalidOperationException(
                    "Missing required database configuration values: " +
                    string.Join(", ", missing) +
                    ". Provide them as environment variables or in a .env file. Example .env entries: SUPABASE_HOST=..., SUPABASE_USER=..., SUPABASE_PASSWORD=..., SUPABASE_DB=...");
            }

            // port defaults to 5432 when not provided or invalid
            var port = 5432;
            if (!string.IsNullOrWhiteSpace(values["SUPABASE_PORT"]) && int.TryParse(values["SUPABASE_PORT"], out var p))
                port = p;

            var host = values["SUPABASE_HOST"]!.Trim();
            var user = values["SUPABASE_USER"]!.Trim();
            var password = values["SUPABASE_PASSWORD"]!.Trim();
            var database = values["SUPABASE_DB"]!.Trim();

            // Build standard Postgres connection string (keep Ssl settings same as before)
            return $"Host={host};Port={port};Database={database};Username={user};Password={password};Ssl Mode=Require;Trust Server Certificate=true;";
        }

        // Try environment first, then fall back to parsing a .env file found upward from base dir
        private static string? GetEnvOrDotEnvValue(string key)
        {
            var env = Environment.GetEnvironmentVariable(key);
            if (!string.IsNullOrWhiteSpace(env))
                return env;

            var dot = FindAndParseDotEnv();
            if (dot != null && dot.TryGetValue(key, out var val) && !string.IsNullOrWhiteSpace(val))
                return val;

            return null;
        }

        private static Dictionary<string, string>? FindAndParseDotEnv()
        {
            var dir = AppContext.BaseDirectory;
            while (!string.IsNullOrEmpty(dir))
            {
                var envPath = Path.Combine(dir, ".env");
                if (File.Exists(envPath))
                    return ParseDotEnv(envPath);

                var parent = Directory.GetParent(dir);
                dir = parent?.FullName;
            }

            return null;
        }

        private static Dictionary<string, string> ParseDotEnv(string path)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var raw in File.ReadAllLines(path))
            {
                var line = raw.Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line.StartsWith("#") || line.StartsWith(";"))
                    continue;

                if (line.StartsWith("export ", StringComparison.OrdinalIgnoreCase))
                    line = line.Substring(7).Trim();

                var idx = line.IndexOf('=');
                if (idx <= 0)
                    continue;

                var key = line.Substring(0, idx).Trim();
                var value = line.Substring(idx + 1).Trim();

                if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                    (value.StartsWith("'") && value.EndsWith("'")))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                dict[key] = value;
            }

            return dict;
        }
    }
}
