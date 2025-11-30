using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; // Diperlukan untuk metode Sum
using System;
using System.Collections.Generic; // Diperlukan untuk inisialisasi koleksi

namespace NelayanGo.ViewModels
{
    public class AnalisisViewModel : INotifyPropertyChanged
    {
        // Data untuk Grafik Garis (Tangkapan Hari Ini)
        public ObservableCollection<TangkapanHarianPoint> DataHarian { get; set; }
        // Data untuk Grafik Batang (Tangkapan Setahun)
        public ObservableCollection<TangkapanTahunanBar> DataTahunan { get; set; }
        // Data untuk Grafik Batang Kecil (Jenis Tangkapan Terbanyak)
        public ObservableCollection<JenisTangkapanBar> DataJenisTangkapan { get; set; }

        // Statistik
        private string _statsHarian = string.Empty;
        private string _statsTahunan = string.Empty;

        public string StatsHarian
        {
            get => _statsHarian;
            set { _statsHarian = value; OnPropertyChanged(nameof(StatsHarian)); }
        }
        public string StatsTahunan
        {
            get => _statsTahunan;
            set { _statsTahunan = value; OnPropertyChanged(nameof(StatsTahunan)); }
        }
        public decimal ProfitHarian { get; set; }

        // PERBAIKAN IDE0028: Sederhanakan inisialisasi koleksi
        public AnalisisViewModel()
        {
            DataHarian = new(LoadDailyData());
            DataTahunan = new(LoadYearlyData());
            DataJenisTangkapan = new(LoadCatchTypeData());

            // Format Statistik
            StatsHarian = GenerateDailyStats();
            StatsTahunan = GenerateYearlyStats();

            ProfitHarian = 500000m; // Rp. 500.000
        }

        // PERBAIKAN CA1822: Ubah ke static karena tidak mengakses data instance
        private static string GenerateDailyStats()
        {
            return $"Jumlah : 45 kg\n" +
                   $"Rerata : 9 kg\n" +
                   $"Waktu mulai : 19.00\n" +
                   $"Waktu selesai : --\n" +
                   $"Durasi : 5:00:00";
        }

        // PERBAIKAN CA1822: Ubah ke static karena tidak mengakses data instance
        private static string GenerateYearlyStats()
        {
            return $"Jumlah : 25.425 ton\n" +
                   $"Rerata : 2.311 ton\n" +
                   $"Jangkauan : 1.936 ton\n" +
                   $"Minimum : 1.353 ton\n" +
                   $"Maximum : 3.289 ton\n" +
                   $"Quartil 1 : 2.543 ton\n" +
                   $"Quartil 2 : 2.786 ton\n" +
                   $"Quartil 3 : 2.468 ton";
        }

        // Load Data methods (dipisah untuk kebersihan)
        private static List<TangkapanHarianPoint> LoadDailyData() => new List<TangkapanHarianPoint>
        {
            new TangkapanHarianPoint { Jam = "19.00", BeratKg = 5 },
            new TangkapanHarianPoint { Jam = "20.00", BeratKg = 18 },
            new TangkapanHarianPoint { Jam = "21.00", BeratKg = 7 },
            new TangkapanHarianPoint { Jam = "22.00", BeratKg = 15 },
            new TangkapanHarianPoint { Jam = "23.00", BeratKg = 10 }
        };

        private static List<TangkapanTahunanBar> LoadYearlyData() => new List<TangkapanTahunanBar>
        {
            new TangkapanTahunanBar { Bulan = "Januari", BeratTon = 2500 },
            new TangkapanTahunanBar { Bulan = "Februari", BeratTon = 1800 },
            new TangkapanTahunanBar { Bulan = "Maret", BeratTon = 2800 },
            new TangkapanTahunanBar { Bulan = "April", BeratTon = 3000 },
            new TangkapanTahunanBar { Bulan = "Mei", BeratTon = 2400 },
            new TangkapanTahunanBar { Bulan = "Juni", BeratTon = 2600 },
            new TangkapanTahunanBar { Bulan = "Juli", BeratTon = 1500 },
            new TangkapanTahunanBar { Bulan = "Agustus", BeratTon = 1800 },
            new TangkapanTahunanBar { Bulan = "September", BeratTon = 2500 },
            new TangkapanTahunanBar { Bulan = "Oktober", BeratTon = 2200 },
            new TangkapanTahunanBar { Bulan = "November", BeratTon = 1300 },
            new TangkapanTahunanBar { Bulan = "Desember", BeratTon = 1800 }
        };

        private static List<JenisTangkapanBar> LoadCatchTypeData() => new List<JenisTangkapanBar>
        {
            new JenisTangkapanBar { NamaIkan = "Bandeng", TotalKg = 2800 },
            new JenisTangkapanBar { NamaIkan = "Bawal", TotalKg = 2500 },
            new JenisTangkapanBar { NamaIkan = "Tongkol", TotalKg = 2000 },
            new JenisTangkapanBar { NamaIkan = "Trenggiri", TotalKg = 1800 },
            new JenisTangkapanBar { NamaIkan = "Teri", TotalKg = 1700 }
        };

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}