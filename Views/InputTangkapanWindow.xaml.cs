using NelayanGo.DataServices;
using NelayanGo.Models;
using NelayanGo.Helpers;
using System.Windows.Input;
using System;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel; // Untuk INotifyPropertyChanged

namespace NelayanGo.Views
{
    // Tambahkan INotifyPropertyChanged
    public partial class InputTangkapanWindow : Window, INotifyPropertyChanged
    {
        private readonly HargaIkanDataService _hargaService = new();
        private readonly NelayanDataService _profilService = new(); // Tambahkan service profil
        private List<HargaIkanModel> _allHargaIkan = new();

        public IkanTangkapanModel? ResultModel { get; private set; }

        // Property untuk Binding Header
        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set
            {
                _currentNelayan = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentNelayan)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public InputTangkapanWindow()
        {
            InitializeComponent();

            // Set DataContext ke diri sendiri agar Binding di XAML bekerja
            this.DataContext = this;

            // Load Data Profil untuk Header
            LoadUserProfile();

            dpTanggal.SelectedDate = DateTime.Now.Date;

            try { _allHargaIkan = _hargaService.GetAll(); }
            catch (Exception ex) { MessageBox.Show($"Gagal memuat daftar harga ikan:\n{ex.Message}"); }

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
                DateTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            };
            timer.Start();
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

        // ... (Kode event handler lain TETAP SAMA, tidak perlu diubah) ...

        private void txtNamaIkan_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = txtNamaIkan.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(text)) { lbNamaIkanSuggest.Visibility = Visibility.Collapsed; lbNamaIkanSuggest.ItemsSource = null; return; }

            var filtered = _allHargaIkan.Where(h => !string.IsNullOrEmpty(h.NamaIkan) && h.NamaIkan.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(h => h.NamaIkan!).Distinct().Take(10).ToList();

            if (filtered.Count == 0) { lbNamaIkanSuggest.Visibility = Visibility.Collapsed; lbNamaIkanSuggest.ItemsSource = null; }
            else { lbNamaIkanSuggest.ItemsSource = filtered; lbNamaIkanSuggest.Visibility = Visibility.Visible; }
        }

        private void lbNamaIkanSuggest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbNamaIkanSuggest.SelectedItem is string selectedNama) { txtNamaIkan.Text = selectedNama; lbNamaIkanSuggest.Visibility = Visibility.Collapsed; }
        }

        private void HomeLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => NavigationHelper.NavigateFromHeaderClick(sender, "Home");
        private void HasilTangkapanLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        private void MapsLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => NavigationHelper.NavigateFromHeaderClick(sender, "Maps");
        private void HargaPasarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => NavigationHelper.NavigateFromHeaderClick(sender, "Harga pasar");
        private void ProfileHeader_Click(object sender, MouseButtonEventArgs e) => NavigationHelper.NavigateFromHeaderClick(sender, "Profil");

        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaIkan.Text) || string.IsNullOrWhiteSpace(txtLokasi.Text) || string.IsNullOrWhiteSpace(txtBeratKg.Text) || string.IsNullOrWhiteSpace(txtJam.Text) || dpTanggal.SelectedDate == null)
            { MessageBox.Show("Semua field wajib diisi."); return; }

            var namaIkan = txtNamaIkan.Text.Trim();
            if (!int.TryParse(txtBeratKg.Text, out var beratKg)) { MessageBox.Show("Berat harus angka."); return; }

            var jamText = txtJam.Text.Trim().Replace('.', ':');
            if (!TimeSpan.TryParseExact(jamText, @"hh\:mm", CultureInfo.InvariantCulture, out var jam)) { MessageBox.Show("Format jam salah (HH:MM)."); return; }

            var tanggal = dpTanggal.SelectedDate.Value.Date;
            var jamTangkap = tanggal.Add(jam);

            HargaIkanModel? hargaIkan = null;
            try { hargaIkan = _hargaService.GetByNamaIkan(namaIkan); }
            catch (Exception ex) { MessageBox.Show($"Gagal data harga ikan:\n{ex.Message}"); return; }

            if (hargaIkan == null) { MessageBox.Show("Nama ikan belum terdaftar di master.", "Info", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            var totalHarga = (long)(hargaIkan.HargaIkan * beratKg);

            ResultModel = new IkanTangkapanModel
            {
                NamaIkan = namaIkan,
                Lokasi = txtLokasi.Text.Trim(),
                BeratKg = beratKg,
                JamTangkap = jamTangkap,
                TotalHargaIkan = totalHarga,
                KodeIkan = hargaIkan.KodeIkan,
                kode_ikan = hargaIkan.KodeIkan
            };

            DialogResult = true; Close();
        }

        private void BatalButton_Click(object sender, RoutedEventArgs e) { DialogResult = false; Close(); }
    }
}