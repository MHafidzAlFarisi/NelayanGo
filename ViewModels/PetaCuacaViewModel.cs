using NelayanGo.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Threading.Tasks;
using NelayanGo.Views;

namespace NelayanGo.ViewModels
{
    public class PetaCuacaViewModel : INotifyPropertyChanged
    {
        // === Data Cuaca ===
        public CuacaModel CuacaAwal { get; set; }
        public CuacaModel CuacaTujuan { get; set; }

        // === Data Koordinat ===
        private string _koordinatPosisiSekarang = "-7.767482997154367, 110.3654515608321";
        private string _koordinatTujuan = "-7.760211743648755, 110.38991330485611";

        public string KoordinatPosisiSekarang
        {
            get => _koordinatPosisiSekarang;
            set { _koordinatPosisiSekarang = value; OnPropertyChanged(nameof(KoordinatPosisiSekarang)); }
        }

        public string KoordinatTujuan
        {
            get => _koordinatTujuan;
            set { _koordinatTujuan = value; OnPropertyChanged(nameof(KoordinatTujuan)); }
        }

        // === Command ===
        public ICommand PilihLokasiCommand { get; }

        public PetaCuacaViewModel()
        {
            CuacaAwal = new CuacaModel();
            CuacaTujuan = new CuacaModel();

            // Inisialisasi Command
            PilihLokasiCommand = new RelayCommand(ExecutePilihLokasi);

            // Muat data cuaca awal (simulasi API)
            SimulateLoadWeather(KoordinatPosisiSekarang, CuacaAwal, "Posisi Awal");
            SimulateLoadWeather(KoordinatTujuan, CuacaTujuan, "Titik Tujuan");
        }

        // Simulasi Logika Panggilan API Cuaca (Dibuat static)
        private static void SimulateLoadWeather(string _, CuacaModel model, string lokasi)
        {
            // DI SINI ADALAH TEMPAT UNTUK MEMANGGIL API CUACA NYATA
            // Contoh: var weatherData = await GetWeatherFromApi(koordinat);

            // SIMULASI DATA
            model.Suhu = "23 °C";
            model.KecepatanAngin = "15 km/h";
            model.ArahAngin = "Utara";
            model.Deskripsi = lokasi; // Deskripsi Cuaca Awal
            model.Presipitasi = "Precipitation: 10%";
            model.Humidity = "Humidity: 88%";
            model.IconEmoji = "🌤️"; // Ikon
        }

        private void ExecutePilihLokasi(object obj)
        {
            // Logika ketika tombol "Pilih Lokasi" diklik
            MessageBox.Show($"Lokasi dipilih:\nPosisi Awal: {KoordinatPosisiSekarang}\nTujuan: {KoordinatTujuan}", "Konfirmasi Lokasi", MessageBoxButton.OK, MessageBoxImage.Information);

            // Dalam aplikasi nyata, Anda akan:
            // 1. Memperbarui pin di peta.
            // 2. Memanggil SimulateLoadWeather untuk koordinat baru.
            // Panggil fungsi static dengan data instance saat ini:
            SimulateLoadWeather(KoordinatPosisiSekarang, CuacaAwal, "Posisi Diperbarui");
            SimulateLoadWeather(KoordinatTujuan, CuacaTujuan, "Tujuan Diperbarui");
        }

        // Implementasi sederhana ICommand (RelayCommand)
        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            public RelayCommand(Action<object> execute) => _execute = execute;
            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter) => _execute(parameter!);
            public event EventHandler? CanExecuteChanged;
        }

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}