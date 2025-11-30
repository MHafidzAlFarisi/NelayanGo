using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NelayanGo.Commands;
using NelayanGo.Helpers;
using NelayanGo.DataServices;
using NelayanGo.Models; // Tambahkan Models

namespace NelayanGo.ViewModels
{
    public class WeatherInfo
    {
        public string Suhu { get; set; } = "-";
        public string KecepatanAngin { get; set; } = "-";
        public string ArahAngin { get; set; } = "-";
        public string Presipitasi { get; set; } = "-";
        public string Humidity { get; set; } = "-";
        public string IconEmoji { get; set; } = "☁️";
    }

    public class PetaCuacaViewModel : INotifyPropertyChanged
    {
        private readonly NelayanDataService _profilService = new(); // Service profil

        // --- DATA HEADER ---
        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(); }
        }

        // --- KOORDINAT ---
        private double _startLat = -6.2088;
        private double _startLng = 106.8456;
        private double _endLat = -5.9;
        private double _endLng = 106.6;

        private WeatherInfo _cuacaAwal;
        private WeatherInfo _cuacaTujuan;
        private int _selectionMode = 0;

        public PetaCuacaViewModel()
        {
            LoadUserProfile(); // Load Header

            CuacaAwal = new WeatherInfo { Suhu = "28°C", KecepatanAngin = "10 km/h", ArahAngin = "Utara", Humidity = "80%" };
            CuacaTujuan = new WeatherInfo { Suhu = "27°C", KecepatanAngin = "15 km/h", ArahAngin = "Barat Laut", Humidity = "85%" };

            PilihLokasiCommand = new RelayCommand((o) => SetCurrentLocation());
        }

        private async void LoadUserProfile()
        {
            if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out long userId))
            {
                var profil = await _profilService.GetProfilByUserId(userId);
                if (profil != null)
                    CurrentNelayan = profil;
                else
                    CurrentNelayan = new NelayanModel { Nama = AppSession.CurrentUser.Username, KodeIdentik = "Belum Input Data" };
            }
            else
            {
                CurrentNelayan = new NelayanModel { Nama = "Tamu", KodeIdentik = "---" };
            }
        }


        public double StartLat
        {
            get => _startLat;
            set { _startLat = value; OnPropertyChanged(); UpdateCuacaAwal(); }
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

        private async void UpdateCuacaAwal()
        {
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
            if (SelectionMode == 0) { StartLat = lat; StartLng = lng; }
            else { EndLat = lat; EndLng = lng; }
        }

        private void SetCurrentLocation()
        {
            StartLat = -7.765752; StartLng = 110.371722;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}