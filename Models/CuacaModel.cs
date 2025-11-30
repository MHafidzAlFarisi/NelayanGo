using System;
using System.ComponentModel;

namespace NelayanGo.Models
{
    // Model untuk menampung data cuaca
    public class CuacaModel : INotifyPropertyChanged
    {
        private string _suhu = "N/A";
        private string _kecepatanAngin = "N/A";
        private string _arahAngin = "N/A";
        private string _deskripsi = "N/A";
        private string _presipitasi = "N/A";
        private string _humidity = "N/A";

        public string Suhu { get => _suhu; set { _suhu = value; OnPropertyChanged(nameof(Suhu)); } }
        public string KecepatanAngin { get => _kecepatanAngin; set { _kecepatanAngin = value; OnPropertyChanged(nameof(KecepatanAngin)); } }
        public string ArahAngin { get => _arahAngin; set { _arahAngin = value; OnPropertyChanged(nameof(ArahAngin)); } }
        public string Deskripsi { get => _deskripsi; set { _deskripsi = value; OnPropertyChanged(nameof(Deskripsi)); } }
        public string Presipitasi { get => _presipitasi; set { _presipitasi = value; OnPropertyChanged(nameof(Presipitasi)); } }
        public string Humidity { get => _humidity; set { _humidity = value; OnPropertyChanged(nameof(Humidity)); } }

        // Simbol ikon (untuk simulasi)
        public string IconEmoji { get; set; } = "☀️";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}