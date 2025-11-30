using NelayanGo.DataServices;
using NelayanGo.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace NelayanGo.Views
{
    public partial class InputTangkapanWindow : Window
    {
        private readonly HargaIkanDataService _hargaService = new();

        private List<HargaIkanModel> _allHargaIkan = new();

        public IkanTangkapanModel? ResultModel { get; private set; }

        public InputTangkapanWindow()
        {
            InitializeComponent();

            dpTanggal.SelectedDate = DateTime.Now.Date;

            // 🔹 Load semua nama ikan sekali saja
            try
            {
                _allHargaIkan = _hargaService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat daftar harga ikan:\n{ex.Message}");
            }

            // Timer buat jam di sisi kanan
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
                DateTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            };
            timer.Start();
        }

        private void txtNamaIkan_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = txtNamaIkan.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(text))
            {
                lbNamaIkanSuggest.Visibility = Visibility.Collapsed;
                lbNamaIkanSuggest.ItemsSource = null;
                return;
            }

            // Filter yang mengandung teks (case-insensitive)
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

        private void lbNamaIkanSuggest_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbNamaIkanSuggest.SelectedItem is string selectedNama)
            {
                txtNamaIkan.Text = selectedNama;
                lbNamaIkanSuggest.Visibility = Visibility.Collapsed;
            }
        }

        private void KirimButton_Click(object sender, RoutedEventArgs e)
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

            // 🔹 Pastikan user login ada
            var currentUser = AppSession.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("Session login tidak ditemukan. Silakan login ulang.");
                return;
            }

            var namaIkan = txtNamaIkan.Text.Trim();

            if (!int.TryParse(txtBeratKg.Text, out var beratKg))
            {
                MessageBox.Show("Berat harus angka.");
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

            // ✅ Kalau ketemu, hitung total harga
            var totalHarga = (long)(hargaIkan.HargaIkan * beratKg);

            ResultModel = new IkanTangkapanModel
            {
                NamaIkan = namaIkan,
                Lokasi = txtLokasi.Text.Trim(),
                BeratKg = beratKg,
                JamTangkap = jamTangkap,
                TotalHargaIkan = totalHarga,
                KodeIkan = hargaIkan.KodeIkan,
                kode_ikan = hargaIkan.KodeIkan,

                ID_User = currentUser.Id,
                TanggalInput = tanggal  
            };

            DialogResult = true;
            Close();
        }

        private void BatalButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
