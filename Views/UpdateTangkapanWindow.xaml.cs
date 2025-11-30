using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NelayanGo.DataServices;
using NelayanGo.Models;
using NelayanGo.Helpers; 
using System.ComponentModel; 
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace NelayanGo.Views
{
    public partial class UpdateTangkapanWindow : Window, INotifyPropertyChanged
    {
        private readonly IkanTangkapanModel _original;
        private readonly HargaIkanDataService _hargaService = new();
        private readonly NelayanDataService _profilService = new(); // Service untuk profil user

        private List<HargaIkanModel> _allHargaIkan = new();

        public IkanTangkapanModel? UpdatedModel { get; private set; }

        // --- PROPERTI BINDING HEADER ---
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

        public UpdateTangkapanWindow(IkanTangkapanModel model)
        {
            InitializeComponent();

            // PENTING: Set DataContext agar Binding di XAML bekerja
            this.DataContext = this;

            // Load Data Profil untuk Header
            LoadUserProfile();

            _original = model;

            // Prefill Data
            txtNamaIkan.Text = _original.NamaIkan;
            txtLokasi.Text = _original.Lokasi;
            dpTanggal.SelectedDate = _original.JamTangkap.Date;
            txtJam.Text = _original.JamTangkap.ToString("HH:mm");
            txtBeratKg.Text = _original.BeratKg.ToString(CultureInfo.InvariantCulture);

            // Timer jam
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
                DateTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            };
            timer.Start();

            // Load Harga
            try
            {
                _allHargaIkan = _hargaService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat daftar harga ikan:\n{ex.Message}");
            }
        }

        // --- LOGIKA LOAD USER PROFILE ---
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

        // --- EVENT HANDLER YANG HILANG (INI SOLUSI UTAMANYA) ---
        private void ProfileHeader_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Profil");
        }
        private void IconLokasi_Click(object sender, MouseButtonEventArgs e)
        {
            // 1. Buat instance window peta picker
            PilihLokasiWindow mapDialog = new PilihLokasiWindow();

            // 2. Tampilkan sebagai Dialog (Pop-up modal)
            // ShowDialog akan mem-pause code di sini sampai window ditutup
            bool? result = mapDialog.ShowDialog();

            // 3. Cek apakah user menekan tombol "PILIH LOKASI INI" (DialogResult = true)
            if (result == true)
            {
                // 4. Ambil koordinat dari property public di window tersebut
                string koordinat = mapDialog.SelectedCoordinateString;

                // 5. Masukkan ke TextBox
                txtLokasi.Text = koordinat;
            }
            
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaIkan.Text) ||
                string.IsNullOrWhiteSpace(txtLokasi.Text) ||
                string.IsNullOrWhiteSpace(txtBeratKg.Text) ||
                string.IsNullOrWhiteSpace(txtJam.Text) ||
                dpTanggal.SelectedDate == null)
            {
                MessageBox.Show("Semua field wajib diisi.");
                return;
            }

            var namaIkan = txtNamaIkan.Text.Trim();

            if (!double.TryParse(txtBeratKg.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var beratKg) || beratKg <= 0)
            {
                MessageBox.Show("Berat KG harus angka > 0.");
                return;
            }

            var jamText = txtJam.Text.Trim().Replace('.', ':');

            if (!TimeSpan.TryParseExact(jamText, @"hh\:mm",
                CultureInfo.InvariantCulture, out var jam))
            {
                MessageBox.Show("Format jam harus HH:MM atau HH.MM, contoh 06:30.");
                return;
            }

            var tanggal = dpTanggal.SelectedDate.Value.Date;
            var jamTangkap = tanggal.Add(jam);

            // Ambil harga ikan
            HargaIkanModel? hargaIkan = null;
            try
            {
                hargaIkan = _hargaService.GetByNamaIkan(namaIkan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil data harga ikan:\n{ex.Message}");
                return;
            }

            if (hargaIkan == null)
            {
                MessageBox.Show(
                    "Nama ikan belum terdaftar di master harga ikan.\n" +
                    "Silakan minta admin untuk menambahkan harga ikan terlebih dahulu.",
                    "Harga Ikan Tidak Ditemukan",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Hitung total harga
            var totalHargaDecimal = hargaIkan.HargaIkan * (decimal)beratKg;
            var totalHarga = (long)totalHargaDecimal;

            UpdatedModel = new IkanTangkapanModel
            {
                kodetangkapan = _original.kodetangkapan,
                ID_User = _original.ID_User,
                KodeIkan = hargaIkan.KodeIkan,
                kode_ikan = hargaIkan.KodeIkan,

                TanggalInput = _original.TanggalInput,
                NamaIkan = namaIkan,
                Lokasi = txtLokasi.Text.Trim(),
                JamTangkap = jamTangkap,
                BeratKg = (int)beratKg, // Cast ke int jika model pakai int
                TotalHargaIkan = totalHarga
            };

            DialogResult = true;
            Close();
        }

        private void BtnBatal_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void txtNamaIkan_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = txtNamaIkan.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(text) || _allHargaIkan.Count == 0)
            {
                lbNamaIkanSuggest.Visibility = Visibility.Collapsed;
                lbNamaIkanSuggest.ItemsSource = null;
                return;
            }

            var filtered = _allHargaIkan
                .Where(h => !string.IsNullOrEmpty(h.NamaIkan) &&
                            h.NamaIkan.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(h => h.NamaIkan!)
                .Distinct()
                .Take(10)
                .ToList();

            if (filtered.Count == 0)
            {
                lbNamaIkanSuggest.Visibility = Visibility.Collapsed;
                lbNamaIkanSuggest.ItemsSource = null;
            }
            else
            {
                lbNamaIkanSuggest.ItemsSource = filtered;
                lbNamaIkanSuggest.Visibility = Visibility.Visible;
            }
        }

        private void lbNamaIkanSuggest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbNamaIkanSuggest.SelectedItem is string selectedNama)
            {
                txtNamaIkan.Text = selectedNama;
                lbNamaIkanSuggest.Visibility = Visibility.Collapsed;
            }
        }
    }
}