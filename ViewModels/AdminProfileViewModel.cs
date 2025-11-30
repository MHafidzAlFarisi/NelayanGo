using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NelayanGo.Models; // Pastikan namespace ini ada

namespace NelayanGo.ViewModels
{
    public class AdminProfileViewModel : INotifyPropertyChanged
    {
        // 1. Ganti 'Nelayan' menjadi 'NelayanModel'
        private NelayanModel? _currentAdmin;

        public NelayanModel? CurrentAdmin
        {
            get => _currentAdmin;
            set { _currentAdmin = value; OnPropertyChanged(nameof(CurrentAdmin)); }
        }

        // 2. Tambahkan Properti Statistik (Karena tidak ada di NelayanModel/Database)
        // Ini sesuai dengan Binding di AdminProfileWindow.xaml Anda sebelumnya

        private string _totalWaktuAktifHari = "0";
        public string TotalWaktuAktifHari
        {
            get => _totalWaktuAktifHari;
            set { _totalWaktuAktifHari = value; OnPropertyChanged(nameof(TotalWaktuAktifHari)); }
        }

        private string _totalHargaIkanDiinput = "0";
        public string TotalHargaIkanDiinput
        {
            get => _totalHargaIkanDiinput;
            set { _totalHargaIkanDiinput = value; OnPropertyChanged(nameof(TotalHargaIkanDiinput)); }
        }

        private string _totalNelayanDiWilayah = "0";
        public string TotalNelayanDiWilayah
        {
            get => _totalNelayanDiWilayah;
            set { _totalNelayanDiWilayah = value; OnPropertyChanged(nameof(TotalNelayanDiWilayah)); }
        }

        public AdminProfileViewModel()
        {
            // Load Data Dummy agar tidak error saat dijalankan
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Set Data Profil Admin
            CurrentAdmin = new NelayanModel
            {
                Nama = "FIKRI HOIRUL (Admin)",
                Wilayah = "Bantul, Yogyakarta",
                Status = "Active",
                NIK = "3400000000000001",
                TanggalLahir = "1995-05-12",
                TempatLahir = "Bantul",
                AlamatKTP = "Jl. Parangtritis Km 12",
                NomorTelepon = "0812-3456-7890",
                Agama = "Islam",
                GolonganDarah = "O",
            };

            // Set Data Statistik
            TotalWaktuAktifHari = "1,245 Hari";
            TotalHargaIkanDiinput = "Rp 4.5 M";
            TotalNelayanDiWilayah = "342 Orang";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}