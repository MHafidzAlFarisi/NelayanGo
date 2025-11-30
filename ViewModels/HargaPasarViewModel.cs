using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.ViewModels
{
    public class HargaPasarViewModel : INotifyPropertyChanged
    {
        private readonly NelayanDataService _profilService = new();
        private readonly HargaIkanDataService _hargaService = new();
        private readonly IkanTangkapanDataService _tangkapanService = new();

        public ObservableCollection<HargaIkanModel> DaftarHarga { get; } = new();
        public ObservableCollection<ProfitModel> DaftarProfit { get; } = new();

        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            private set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        private decimal _totalPendapatan;
        public decimal TotalPendapatan
        {
            get => _totalPendapatan;
            private set
            {
                _totalPendapatan = value;
                OnPropertyChanged(nameof(TotalPendapatan));
            }
        }

        public HargaPasarViewModel()
        {
            LoadUserProfile(); // Header
            LoadHargaIkan();   // Data harga pasar
            LoadProfitHariIni(); // Profit berdasarkan tangkapan hari ini
        }

        private async void LoadUserProfile()
        {
            if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out var userId))
            {
                var profil = await _profilService.GetProfilByUserId(userId);
                CurrentNelayan = profil ?? new NelayanModel { Nama = AppSession.CurrentUser.Username, KodeIdentik = "Belum Input Data" };
            }
            else
            {
                CurrentNelayan = new NelayanModel { Nama = "Tamu", KodeIdentik = "---" };
            }
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

                if (AppSession.CurrentUser == null || !long.TryParse(AppSession.CurrentUser.Id, out var userId))
                {
                    Console.WriteLine("Tidak ada user login, daftar profit kosong.");
                    return;
                }

                var today = DateTime.Today;
                var tangkapanList = _tangkapanService.GetByUserAndDate(userId, today);

                foreach (var t in tangkapanList)
                {
                    var item = new ProfitModel
                    {
                        NamaIkan = t.NamaIkan,
                        BeratTangkapan = Convert.ToDecimal(t.BeratKg),
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
