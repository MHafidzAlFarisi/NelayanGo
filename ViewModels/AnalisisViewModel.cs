using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;

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
        public string StatsHarian { get; set; }
        public string StatsTahunan { get; set; }
        public decimal ProfitHarian { get; set; }

        public AnalisisViewModel()
        {
            DataHarian = new ObservableCollection<TangkapanHarianPoint>();
            DataTahunan = new ObservableCollection<TangkapanTahunanBar>();
            DataJenisTangkapan = new ObservableCollection<JenisTangkapanBar>();

            LoadData();

            // Format Statistik
            StatsHarian = GenerateDailyStats();
            StatsTahunan = GenerateYearlyStats();
        }

        private void LoadData()
        {
            // Data Grafik Garis (TANGKAPAN HARI INI)
            DataHarian.Add(new TangkapanHarianPoint { Jam = "19.00", BeratKg = 5 });
            DataHarian.Add(new TangkapanHarianPoint { Jam = "20.00", BeratKg = 18 });
            DataHarian.Add(new TangkapanHarianPoint { Jam = "21.00", BeratKg = 7 });
            DataHarian.Add(new TangkapanHarianPoint { Jam = "22.00", BeratKg = 15 });
            DataHarian.Add(new TangkapanHarianPoint { Jam = "23.00", BeratKg = 10 });

            // Profit Harian
            ProfitHarian = 500000m; // Rp. 500.000

            // Data Grafik Batang (TANGKAPAN SETAHUN)
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Januari", BeratTon = 2500 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Februari", BeratTon = 1800 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Maret", BeratTon = 2800 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "April", BeratTon = 3000 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Mei", BeratTon = 2400 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Juni", BeratTon = 2600 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Juli", BeratTon = 1500 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Agustus", BeratTon = 1800 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "September", BeratTon = 2500 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Oktober", BeratTon = 2200 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "November", BeratTon = 1300 });
            DataTahunan.Add(new TangkapanTahunanBar { Bulan = "Desember", BeratTon = 1800 }); // Placeholder

            // Data Grafik Jenis Tangkapan
            DataJenisTangkapan.Add(new JenisTangkapanBar { NamaIkan = "Bandeng", TotalKg = 2800 });
            DataJenisTangkapan.Add(new JenisTangkapanBar { NamaIkan = "Bawal", TotalKg = 2500 });
            DataJenisTangkapan.Add(new JenisTangkapanBar { NamaIkan = "Tongkol", TotalKg = 2000 });
            DataJenisTangkapan.Add(new JenisTangkapanBar { NamaIkan = "Trenggiri", TotalKg = 1800 });
            DataJenisTangkapan.Add(new JenisTangkapanBar { NamaIkan = "Teri", TotalKg = 1700 });
        }

        private string GenerateDailyStats()
        {
            return $"Jumlah : 45 kg\n" +
                   $"Rerata : 9 kg\n" +
                   $"Waktu mulai : 19.00\n" +
                   $"Waktu selesai : --\n" +
                   $"Durasi : 5:00:00";
        }

        private string GenerateYearlyStats()
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

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}