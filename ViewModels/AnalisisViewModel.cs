using NelayanGo.Models;
using NelayanGo.DataServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Threading.Tasks;

namespace NelayanGo.ViewModels
{
    public class AnalisisViewModel : INotifyPropertyChanged
    {
        private readonly IkanTangkapanDataService _service = new();
        private readonly NelayanDataService _profilService = new();

        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        // ... (Properti Lainnya Tetap Sama, disingkat agar fokus) ...
        public ObservableCollection<TangkapanHarianPoint> DataHarian { get; set; } = new();
        public ObservableCollection<TangkapanTahunanBar> DataTahunan { get; set; } = new();
        public ObservableCollection<JenisTangkapanBar> DataJenisTangkapan { get; set; } = new();

        private string _statsHarian = "-";
        public string StatsHarian
        {
            get => _statsHarian;
            set { _statsHarian = value; OnPropertyChanged(nameof(StatsHarian)); }
        }

        private string _statsTahunan = "-";
        public string StatsTahunan
        {
            get => _statsTahunan;
            set { _statsTahunan = value; OnPropertyChanged(nameof(StatsTahunan)); }
        }

        private decimal _profitHarian;
        public decimal ProfitHarian
        {
            get => _profitHarian;
            set { _profitHarian = value; OnPropertyChanged(nameof(ProfitHarian)); }
        }

        public AnalisisViewModel()
        {
            // [PENTING] Set nilai awal agar tidak kosong/blank saat loading
            CurrentNelayan = new NelayanModel
            {
                Nama = "Memuat...",
                KodeIdentik = "..."
            };

            LoadRealData();
            LoadUserProfile();
        }

        private async void LoadUserProfile()
        {
            // Cek apakah ada user yang login di sesi ini
            if (AppSession.CurrentUser != null)
            {
                string usernameLogin = AppSession.CurrentUser.Username;

                if (long.TryParse(AppSession.CurrentUser.Id, out long userId))
                {
                    // Ambil data profil asli dari database
                    var profil = await _profilService.GetProfilByUserId(userId);

                    if (profil != null)
                    {
                        // Jika ada profil, tampilkan
                        CurrentNelayan = profil;
                    }
                    else
                    {
                        // Jika profil belum diisi, tampilkan Username dari Login
                        CurrentNelayan = new NelayanModel
                        {
                            Nama = usernameLogin,
                            KodeIdentik = "Belum Input Data"
                        };
                    }
                }
            }
            else
            {
                // Jika tidak ada sesi login (misal debug langsung ke window ini)
                CurrentNelayan = new NelayanModel
                {
                    Nama = "Tamu (Debug)",
                    KodeIdentik = "---"
                };
            }
        }

        // ... (Sisa fungsi LoadRealData, ProcessDailyData, dll SAMA PERSIS seperti sebelumnya) ...
        // ... (Pastikan fungsi-fungsi tersebut tetap ada di file Anda) ...

        public void LoadRealData()
        {
            try
            {
                long userId = 1;
                if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out long parsedId))
                {
                    userId = parsedId;
                }

                var rawData = _service.GetByUser(userId);
                if (rawData == null || !rawData.Any())
                {
                    StatsHarian = "Belum ada data.";
                    return;
                }

                ProcessDailyData(rawData);
                ProcessYearlyData(rawData);
                ProcessCatchTypeData(rawData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error Load Analisis: {ex.Message}");
            }
        }

        private void ProcessDailyData(List<IkanTangkapanModel> rawData)
        {
            var today = DateTime.Now.Date;
            var todaysCatch = rawData.Where(x => x.JamTangkap.Date == today).ToList();

            var grouped = todaysCatch
                .GroupBy(x => x.JamTangkap.Hour)
                .Select(g => new TangkapanHarianPoint
                {
                    Jam = $"{g.Key:00}.00",
                    BeratKg = g.Sum(x => x.BeratKg)
                })
                .OrderBy(x => x.Jam)
                .ToList();

            DataHarian.Clear();
            foreach (var item in grouped) DataHarian.Add(item);

            if (todaysCatch.Any())
            {
                double totalBerat = todaysCatch.Sum(x => x.BeratKg);
                double rerata = totalBerat / todaysCatch.Count;
                ProfitHarian = todaysCatch.Sum(x => (decimal)x.TotalHargaIkan);

                StatsHarian = $"Jumlah : {totalBerat} kg\n" +
                              $"Rerata : {rerata:F1} kg\n" +
                              $"Total : Rp {ProfitHarian:N0}";
            }
            else
            {
                StatsHarian = "Tidak ada tangkapan hari ini.";
                ProfitHarian = 0;
            }
        }

        private void ProcessYearlyData(List<IkanTangkapanModel> rawData)
        {
            var thisYear = DateTime.Now.Year;
            var yearsCatch = rawData.Where(x => x.JamTangkap.Year == thisYear).ToList();

            var grouped = yearsCatch
                .GroupBy(x => x.JamTangkap.Month)
                .Select(g => new TangkapanTahunanBar
                {
                    Bulan = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key),
                    BeratTon = (double)g.Sum(x => x.BeratKg) / 1000.0
                })
                .ToList();

            DataTahunan.Clear();
            foreach (var item in grouped) DataTahunan.Add(item);

            if (yearsCatch.Any())
            {
                double totalTon = yearsCatch.Sum(x => x.BeratKg) / 1000.0;
                StatsTahunan = $"Total Tahun Ini : {totalTon:F3} ton";
            }
        }

        private void ProcessCatchTypeData(List<IkanTangkapanModel> rawData)
        {
            var grouped = rawData
                .GroupBy(x => x.NamaIkan)
                .Select(g => new JenisTangkapanBar
                {
                    NamaIkan = g.Key,
                    TotalKg = g.Sum(x => x.BeratKg)
                })
                .OrderByDescending(x => x.TotalKg)
                .Take(5)
                .ToList();

            DataJenisTangkapan.Clear();
            foreach (var item in grouped) DataJenisTangkapan.Add(item);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}