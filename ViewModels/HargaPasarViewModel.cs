using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; // Diperlukan untuk metode Sum
using System;

namespace NelayanGo.ViewModels
{
    public class HargaPasarViewModel : INotifyPropertyChanged
    {
        // Data untuk tabel DAFTAR HARGA IKAN (Kiri)
        public ObservableCollection<HargaIkanModel> DaftarHarga { get; set; }

        // Data untuk tabel PROFIT TANGKAPAN HARI INI (Kanan)
        public ObservableCollection<ProfitModel> DaftarProfit { get; set; }

        private decimal _totalPendapatan;

        // Properti yang di-bind untuk total pendapatan
        public decimal TotalPendapatan
        {
            get => _totalPendapatan;
            set
            {
                _totalPendapatan = value;
                OnPropertyChanged(nameof(TotalPendapatan));
            }
        }

        public HargaPasarViewModel()
        {
            DaftarHarga = new ObservableCollection<HargaIkanModel>();
            DaftarProfit = new ObservableCollection<ProfitModel>();
            LoadData();
        }

        private void LoadData()
        {
            // 1. Muat Data Harga Ikan (Kiri)
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Tongkol", HargaPerKilo = 12000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Kakap", HargaPerKilo = 13000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Bandeng", HargaPerKilo = 11000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Kerapu", HargaPerKilo = 15000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Trenggiri", HargaPerKilo = 14000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Layang", HargaPerKilo = 13000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Teri", HargaPerKilo = 10000 });

            // 2. Muat Data Profit (Kanan) - Menggunakan data statis seperti di gambar
            // Catatan: Dalam aplikasi nyata, data ini harus dihitung dari DaftarHarga dan data tangkapan.
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Tongkol", BeratTangkapan = 10.00m, Harga = 120000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Kakap", BeratTangkapan = 10.00m, Harga = 130000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Bandeng", BeratTangkapan = 10.00m, Harga = 110000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Kerapu", BeratTangkapan = 10.00m, Harga = 150000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Trenggiri", BeratTangkapan = 10.00m, Harga = 140000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Layang", BeratTangkapan = 10.00m, Harga = 130000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Teri", BeratTangkapan = 10.00m, Harga = 100000 });

            // 3. Hitung Total Pendapatan
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            // Menghitung total dari kolom Harga di DaftarProfit
            // Pastikan Anda telah menginstal System.Linq jika menggunakan metode Sum()
            TotalPendapatan = DaftarProfit.Sum(p => p.Harga);
        }

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}