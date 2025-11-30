using NelayanGo.DataServices;
using NelayanGo.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace NelayanGo.ViewModels
{
    public class HargaPasarViewModel : INotifyPropertyChanged
    {
        private readonly HargaIkanDataService _hargaService = new();
        private readonly IkanTangkapanDataService _tangkapanService = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        // Tabel kiri
        public ObservableCollection<HargaIkanModel> DaftarHarga { get; } = new();

        // Tabel kanan
        public ObservableCollection<HargaPasarProfitItem> DaftarProfit { get; } = new();

        private decimal _totalPendapatan;
        public decimal TotalPendapatan
        {
            get => _totalPendapatan;
            private set
            {
                if (_totalPendapatan != value)
                {
                    _totalPendapatan = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPendapatan)));
                }
            }
        }

        public HargaPasarViewModel()
        {
            LoadHargaIkan();
            LoadProfitHariIni();
        }

        private void LoadHargaIkan()
        {
            try
            {
                DaftarHarga.Clear();
                var list = _hargaService.GetAll();
                foreach (var h in list)
                    DaftarHarga.Add(h);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat harga ikan: {ex.Message}");
            }
        }

        private void LoadProfitHariIni()
        {
            try
            {
                DaftarProfit.Clear();
                TotalPendapatan = 0;

                var user = AppSession.CurrentUser;
                if (user == null)
                {
                    Console.WriteLine("Tidak ada user login, daftar profit kosong.");
                    return;
                }

                var today = DateTime.Today;

                var tangkapanList = _tangkapanService.GetByUserAndDate(user.Id, today);

                foreach (var t in tangkapanList)
                {
                    // mapping sesuai model-mu
                    var item = new HargaPasarProfitItem
                    {
                        NamaIkan = t.NamaIkan,
                        BeratTangkapan = t.BeratKg,               
                        Harga = Convert.ToDecimal(t.TotalHargaIkan) 
                    };

                    DaftarProfit.Add(item);
                }

                TotalPendapatan = DaftarProfit.Sum(p => p.Harga);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat profit tangkapan hari ini: {ex.Message}");
            }
        }
    }
}
