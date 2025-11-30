using NelayanGo.Models;
using NelayanGo.DataServices; // Tambahkan namespace DataServices
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;

namespace NelayanGo.ViewModels
{
    public class HargaPasarViewModel : INotifyPropertyChanged
    {
        private readonly NelayanDataService _profilService = new(); // Service profil

        public ObservableCollection<HargaIkanModel> DaftarHarga { get; set; }
        public ObservableCollection<ProfitModel> DaftarProfit { get; set; }

        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        private decimal _totalPendapatan;
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

            LoadUserProfile(); // Load Header
            LoadData();
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

        private void LoadData()
        {
            // Data Dummy Harga & Profit (Sama seperti sebelumnya)
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Tongkol", HargaIkan = 12000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Kakap", HargaIkan = 13000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Bandeng", HargaIkan = 11000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Kerapu", HargaIkan = 15000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Trenggiri", HargaIkan = 14000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Layang", HargaIkan = 13000 });
            DaftarHarga.Add(new HargaIkanModel { NamaIkan = "Teri", HargaIkan = 10000 });

            DaftarProfit.Add(new ProfitModel { NamaIkan = "Tongkol", BeratTangkapan = 10.00m, Harga = 120000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Kakap", BeratTangkapan = 10.00m, Harga = 130000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Bandeng", BeratTangkapan = 10.00m, Harga = 110000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Kerapu", BeratTangkapan = 10.00m, Harga = 150000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Trenggiri", BeratTangkapan = 10.00m, Harga = 140000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Layang", BeratTangkapan = 10.00m, Harga = 130000 });
            DaftarProfit.Add(new ProfitModel { NamaIkan = "Teri", BeratTangkapan = 10.00m, Harga = 100000 });

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalPendapatan = DaftarProfit.Sum(p => p.Harga);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}