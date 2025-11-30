using NelayanGo.DataServices;
using NelayanGo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace NelayanGo.ViewModels
{
    public class AnalisisViewModel : INotifyPropertyChanged
    {
        private readonly IkanTangkapanDataService _tangkapanService = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        // ===== PROPERTY TEKS =====
        private decimal _profitHarian;
        public decimal ProfitHarian
        {
            get => _profitHarian;
            private set { if (_profitHarian != value) { _profitHarian = value; OnPropertyChanged(nameof(ProfitHarian)); } }
        }

        private string _statsHarian = "Belum ada data tangkapan hari ini.";
        public string StatsHarian
        {
            get => _statsHarian;
            private set { if (_statsHarian != value) { _statsHarian = value; OnPropertyChanged(nameof(StatsHarian)); } }
        }

        private string _statsTahunan = "Belum ada data tangkapan tahun ini.";
        public string StatsTahunan
        {
            get => _statsTahunan;
            private set { if (_statsTahunan != value) { _statsTahunan = value; OnPropertyChanged(nameof(StatsTahunan)); } }
        }

        // ===== PLOT MODEL =====
        private PlotModel _plotHarian = new PlotModel { Title = "Tangkapan Harian" };
        public PlotModel PlotHarian
        {
            get => _plotHarian;
            private set { _plotHarian = value; OnPropertyChanged(nameof(PlotHarian)); }
        }

        private PlotModel _plotTahunan = new PlotModel { Title = "Tangkapan Tahunan" };
        public PlotModel PlotTahunan
        {
            get => _plotTahunan;
            private set { _plotTahunan = value; OnPropertyChanged(nameof(PlotTahunan)); }
        }

        private PlotModel _plotJenis = new PlotModel { Title = "Jenis Tangkapan Terbanyak" };
        public PlotModel PlotJenis
        {
            get => _plotJenis;
            private set { _plotJenis = value; OnPropertyChanged(nameof(PlotJenis)); }
        }

        public AnalisisViewModel()
        {
            LoadAnalisis();
        }

        private void LoadAnalisis()
        {
            var user = AppSession.CurrentUser;
            if (user == null)
            {
                StatsHarian = "Silakan login untuk melihat analisis tangkapan.";
                StatsTahunan = "Silakan login untuk melihat analisis tangkapan.";
                ProfitHarian = 0;
                SetupEmptyPlots();
                return;
            }

            var allData = _tangkapanService.GetByUser(user.Id);

            if (allData == null || allData.Count == 0)
            {
                StatsHarian = "Belum ada data tangkapan.";
                StatsTahunan = "Belum ada data tangkapan.";
                ProfitHarian = 0;
                SetupEmptyPlots();
                return;
            }

            var today = DateTime.Today;
            var thisYear = today.Year;

            // ===================== HARIAN =====================
            var dataHarian = allData
                .Where(t => t.JamTangkap.Date == today)
                .ToList();

            if (dataHarian.Count == 0)
            {
                StatsHarian = "Belum ada tangkapan yang dicatat untuk hari ini.";
                ProfitHarian = 0;
                PlotHarian = CreateEmptyPlot("Tangkapan Harian");
            }
            else
            {
                var totalKgHarian = dataHarian.Sum(t => (double)t.BeratKg);
                var totalRupiahHarian = dataHarian.Sum(t => (decimal)t.TotalHargaIkan);

                var jenisHarian = dataHarian
                    .GroupBy(t => t.NamaIkan)
                    .OrderByDescending(g => g.Sum(x => x.BeratKg))
                    .First().Key;

                ProfitHarian = totalRupiahHarian;

                StatsHarian =
                    $"• Total tangkapan hari ini: {totalKgHarian:N2} kg\n" +
                    $"• Total transaksi: {dataHarian.Count} kali\n" +
                    $"• Ikan terbanyak: {jenisHarian}\n" +
                    $"• Total pendapatan: Rp {totalRupiahHarian:N0}";

                PlotHarian = CreateHarianPlot(dataHarian);
            }

            // ===================== TAHUNAN =====================
            var dataTahunan = allData
                .Where(t => t.JamTangkap.Year == thisYear)
                .ToList();

            if (dataTahunan.Count == 0)
            {
                StatsTahunan = "Belum ada data tangkapan pada tahun ini.";
                PlotTahunan = CreateEmptyPlot("Tangkapan Tahunan");
            }
            else
            {
                var totalKgTahun = dataTahunan.Sum(t => (double)t.BeratKg);
                var totalRupiahTahun = dataTahunan.Sum(t => (decimal)t.TotalHargaIkan);

                var grupBulan = dataTahunan
                    .GroupBy(t => t.JamTangkap.Month)
                    .Select(g => new
                    {
                        Bulan = g.Key,
                        Pendapatan = g.Sum(x => (decimal)x.TotalHargaIkan)
                    })
                    .OrderByDescending(x => x.Pendapatan)
                    .First();

                var namaBulan = new DateTime(thisYear, grupBulan.Bulan, 1)
                    .ToString("MMMM", new CultureInfo("id-ID"));

                var jenisTahunan = dataTahunan
                    .GroupBy(t => t.NamaIkan)
                    .OrderByDescending(g => g.Sum(x => x.BeratKg))
                    .First().Key;

                StatsTahunan =
                    $"• Total tangkapan tahun ini: {totalKgTahun:N2} kg\n" +
                    $"• Total pendapatan: Rp {totalRupiahTahun:N0}\n" +
                    $"• Bulan tertinggi: {namaBulan}\n" +
                    $"• Ikan paling sering ditangkap: {jenisTahunan}";

                PlotTahunan = CreateTahunanPlot(dataTahunan, thisYear);
            }

            // ===================== JENIS TANGKAPAN (TAHUN INI) =====================
            PlotJenis = CreateJenisPlot(
                dataTahunan.Count > 0 ? dataTahunan : allData);
        }

        // -------- Plot Helpers --------

        private PlotModel CreateEmptyPlot(string title)
        {
            var pm = new PlotModel { Title = title };
            pm.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom });
            pm.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0 });
            return pm;
        }

        private PlotModel CreateHarianPlot(List<IkanTangkapanModel> dataHarian)
        {
            var pm = new PlotModel { Title = "Tangkapan Harian (kg per jam)" };

            var grouped = dataHarian
                .GroupBy(t => t.JamTangkap.Hour)
                .OrderBy(g => g.Key)
                .ToList();

            var catAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Title = "Kg" };

            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4
            };

            int index = 0;
            foreach (var g in grouped)
            {
                var label = $"{g.Key:00}.00";
                catAxis.Labels.Add(label);

                double kg = g.Sum(x => (double)x.BeratKg);
                series.Points.Add(new DataPoint(index, kg));
                index++;
            }

            pm.Axes.Add(catAxis);
            pm.Axes.Add(valueAxis);
            pm.Series.Add(series);

            return pm;
        }

        private PlotModel CreateTahunanPlot(List<IkanTangkapanModel> dataTahunan, int year)
        {
            var pm = new PlotModel { Title = "Tangkapan Tahunan (kg per bulan)" };
            var culture = new CultureInfo("id-ID");

            var grouped = dataTahunan
                .GroupBy(t => t.JamTangkap.Month)
                .OrderBy(g => g.Key)
                .ToList();

            var catAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Title = "Kg" };

            // pakai LineSeries, bukan ColumnSeries
            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };

            int index = 0;
            foreach (var g in grouped)
            {
                string label = new DateTime(year, g.Key, 1).ToString("MMM", culture);
                catAxis.Labels.Add(label);

                double kg = g.Sum(x => (double)x.BeratKg);
                series.Points.Add(new DataPoint(index, kg));
                index++;
            }

            pm.Axes.Add(catAxis);
            pm.Axes.Add(valueAxis);
            pm.Series.Add(series);

            return pm;
        }

        private PlotModel CreateJenisPlot(List<IkanTangkapanModel> data)
        {
            var pm = new PlotModel { Title = "Jenis Tangkapan (kg)" };

            if (data.Count == 0)
                return pm;

            var grouped = data
                .GroupBy(t => string.IsNullOrWhiteSpace(t.NamaIkan) ? "(Tidak diketahui)" : t.NamaIkan)
                .OrderByDescending(g => g.Sum(x => x.BeratKg))
                .Take(6)
                .ToList();

            var catAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Title = "Kg" };

            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };

            int index = 0;
            foreach (var g in grouped)
            {
                catAxis.Labels.Add(g.Key);
                double kg = g.Sum(x => (double)x.BeratKg);
                series.Points.Add(new DataPoint(index, kg));
                index++;
            }

            pm.Axes.Add(catAxis);
            pm.Axes.Add(valueAxis);
            pm.Series.Add(series);

            return pm;
        }


        private void SetupEmptyPlots()
        {
            PlotHarian = CreateEmptyPlot("Tangkapan Harian");
            PlotTahunan = CreateEmptyPlot("Tangkapan Tahunan");
            PlotJenis = CreateEmptyPlot("Jenis Tangkapan Terbanyak");
        }

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
