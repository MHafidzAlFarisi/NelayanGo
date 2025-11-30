using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NelayanGo.DataServices
{
    public class WeatherModel
    {
        public string Temperature { get; set; } = "-";
        public string WindSpeed { get; set; } = "-";
        public string Description { get; set; } = "Belum ada data";
        public string IconUrl { get; set; } = ""; // URL gambar awan/matahari
        public string Humidity { get; set; } = "-";
    }

    public static class WeatherService
    {
        private const string API_KEY = "MASUKKAN_API_KEY_OPENWEATHERMAP_DISINI";

        // Fungsi untuk mendapatkan cuaca (Simulasi/Mockup agar langsung jalan tanpa API Key)
        // Jika ingin data asli, uncomment bagian HTTP Client
        public static async Task<WeatherModel> GetWeatherAsync(double lat, double lng)
        {
            // Simulasi Delay Network
            await Task.Delay(500);

            // LOGIKA SIMULASI (Supaya terlihat berubah-ubah sesuai lokasi)
            // Di aplikasi asli, Anda akan melakukan Request ke API OpenWeatherMap
            var random = new Random((int)(lat + lng));
            int temp = random.Next(20, 32);
            int wind = random.Next(5, 25);

            string[] conditions = { "Cerah", "Berawan", "Hujan Ringan", "Badai Petir" };
            string condition = conditions[random.Next(0, 4)];

            return new WeatherModel
            {
                Temperature = $"{temp}°C",
                WindSpeed = $"{wind} km/h",
                Description = condition,
                Humidity = $"{random.Next(60, 90)}%",
                // Icon placeholder (bisa diganti URL icon asli)
                IconUrl = "pack://application:,,,/NelayanGo;component/Assets/weather_placeholder.png"
            };
        }

        /* * JIKA SUDAH PUNYA API KEY, GUNAKAN KODE INI:
         * public static async Task<WeatherModel> GetRealWeather(double lat, double lng)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lng}&appid={API_KEY}&units=metric&lang=id";
                var response = await client.GetStringAsync(url);
                var data = JObject.Parse(response);

                return new WeatherModel
                {
                    Temperature = $"{data["main"]["temp"]}°C",
                    WindSpeed = $"{data["wind"]["speed"]} km/h",
                    Description = data["weather"][0]["description"].ToString(),
                    Humidity = $"{data["main"]["humidity"]}%"
                };
            }
        }
        */
    }
}