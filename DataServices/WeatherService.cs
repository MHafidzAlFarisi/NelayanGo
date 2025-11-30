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
        public string IconUrl { get; set; } = ""; 
        public string Humidity { get; set; } = "-";
    }

    public static class WeatherService
    {
        private const string API_KEY = "13d352asiegni3";

        // Fungsi untuk mendapatkan cuaca
        public static async Task<WeatherModel> GetWeatherAsync(double lat, double lng)
        {
            // Simulasi Delay Network
            await Task.Delay(500);

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
                // Icon placeholder
                IconUrl = "pack://application:,,,/NelayanGo;component/Assets/weather_placeholder.png"
            };
        }
    }
}