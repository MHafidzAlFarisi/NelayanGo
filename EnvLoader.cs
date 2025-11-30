using System;
using System.IO;

namespace NelayanGo.Utils
{
    public static class EnvLoader
    {
        private static bool _loaded = false;

        public static void Load()
        {
            if (_loaded) return;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = Path.Combine(baseDir, ".env");

            if (!File.Exists(envPath))
                return; // kalau nggak ada ya sudah, pakai default

            foreach (var rawLine in File.ReadAllLines(envPath))
            {
                var line = rawLine.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;

                var idx = line.IndexOf('=');
                if (idx <= 0) continue;

                var key = line[..idx].Trim();
                var value = line[(idx + 1)..].Trim();

                Environment.SetEnvironmentVariable(key, value);
            }

            _loaded = true;
        }
    }
}
