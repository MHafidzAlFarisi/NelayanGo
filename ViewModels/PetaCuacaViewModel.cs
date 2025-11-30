using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NelayanGo.DataServices; // Pastikan namespace ini benar

namespace NelayanGo.ViewModels
{
    // Model Data Cuaca Sederhana
    public class WeatherInfo
    {
        public string Suhu { get; set; } = "-";
        public string KecepatanAngin { get; set; } = "-";
        public string ArahAngin { get; set; } = "-";
        public string Presipitasi { get; set; } = "-";
        public string Humidity { get; set; } = "-";
        public string IconEmoji { get; set; } = "☁️"; // Emoji sebagai placeholder icon
    }

    public class PetaCuacaViewModel : INotifyPropertyChanged
    {
        // --- KOORDINAT (Double agar bisa diolah GMap) ---
        private double _startLat = -6.2088; // Default Jakarta
        private double _startLng = 106.8456;
        private double _endLat = -5.9; // Default Kepulauan Seribu
        private double _endLng = 106.6;

        // --- DATA CUACA (Objek Terpisah) ---
        private WeatherInfo _cuacaAwal;
        private WeatherInfo _cuacaTujuan;

        // --- MODE KLIK (0 = Awal, 1 = Tujuan) ---
        private int _selectionMode = 0;

        public PetaCuacaViewModel()
        {
            // Inisialisasi Data Dummy Awal
            CuacaAwal = new WeatherInfo { Suhu = "28°C", KecepatanAngin = "10 km/h", ArahAngin = "Utara", Humidity = "80%" };
            CuacaTujuan = new WeatherInfo { Suhu = "27°C", KecepatanAngin = "15 km/h", ArahAngin = "Barat Laut", Humidity = "85%" };

            PilihLokasiCommand = new RelayCommand((o) => SetCurrentLocation());
        }

        // --- Properti Binding ---
        public double StartLat
        {
            get => _startLat;
            set { _startLat = value; OnPropertyChanged(); UpdateCuacaAwal(); } // Simulasi update saat ganti
        }
        public double StartLng
        {
            get => _startLng;
            set { _startLng = value; OnPropertyChanged(); UpdateCuacaAwal(); }
        }

        public double EndLat
        {
            get => _endLat;
            set { _endLat = value; OnPropertyChanged(); UpdateCuacaTujuan(); }
        }
        public double EndLng
        {
            get => _endLng;
            set { _endLng = value; OnPropertyChanged(); UpdateCuacaTujuan(); }
        }

        public WeatherInfo CuacaAwal
        {
            get => _cuacaAwal;
            set { _cuacaAwal = value; OnPropertyChanged(); }
        }

        public WeatherInfo CuacaTujuan
        {
            get => _cuacaTujuan;
            set { _cuacaTujuan = value; OnPropertyChanged(); }
        }

        public int SelectionMode
        {
            get => _selectionMode;
            set { _selectionMode = value; OnPropertyChanged(); }
        }

        public ICommand PilihLokasiCommand { get; }

        // --- Logika Update (Simulasi) ---
        // Di aplikasi nyata, panggil API WeatherService di sini
        private async void UpdateCuacaAwal()
        {
            // Simulasi loading/perubahan data berdasarkan koordinat
            await Task.Delay(200);
            var random = new Random((int)StartLat);
            CuacaAwal = new WeatherInfo
            {
                Suhu = $"{random.Next(25, 33)}°C",
                KecepatanAngin = $"{random.Next(5, 20)} km/h",
                ArahAngin = "Barat Daya",
                Humidity = $"{random.Next(70, 95)}%",
                IconEmoji = random.Next(0, 2) == 0 ? "☀️" : "☁️"
            };
        }

        private async void UpdateCuacaTujuan()
        {
            await Task.Delay(200);
            var random = new Random((int)EndLat);
            CuacaTujuan = new WeatherInfo
            {
                Suhu = $"{random.Next(24, 30)}°C",
                KecepatanAngin = $"{random.Next(10, 30)} km/h",
                ArahAngin = "Utara",
                Humidity = $"{random.Next(60, 85)}%",
                IconEmoji = "⛈️"
            };
        }

        public void SetLocationFromMap(double lat, double lng)
        {
            if (SelectionMode == 0)
            {
                StartLat = lat;
                StartLng = lng;
            }
            else
            {
                EndLat = lat;
                EndLng = lng;
            }
        }

        private void SetCurrentLocation()
        {
            // Simulasi lokasi saat ini (Monas)
            StartLat = -6.1754;
            StartLng = 106.8272;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}