using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.Views
{
    public partial class UpdateTangkapanWindow : Window
    {
        private readonly IkanTangkapanModel _original;
        private readonly HargaIkanDataService _hargaService = new();

        private List<HargaIkanModel> _allHargaIkan = new();

        public IkanTangkapanModel? UpdatedModel { get; private set; }

        public UpdateTangkapanWindow(IkanTangkapanModel model)
        {
            InitializeComponent();

            _original = model;

            // Prefill
            txtNamaIkan.Text = _original.NamaIkan;
            txtLokasi.Text = _original.Lokasi;
            dpTanggal.SelectedDate = _original.JamTangkap.Date;
            txtJam.Text = _original.JamTangkap.ToString("HH:mm");
            txtBeratKg.Text = _original.BeratKg.ToString(CultureInfo.InvariantCulture);

            // Timer jam / tanggal (optional)
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
                DateTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy");
            };
            timer.Start();

            // Load semua harga ikan untuk suggestion
            try
            {
                _allHargaIkan = _hargaService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat daftar harga ikan:\n{ex.Message}");
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

            if (!int.TryParse(txtBeratKg.Text, out var beratKg) || beratKg <= 0)
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

            // 🔍 Ambil harga ikan dari master HargaIkan (Supabase)
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

            // ✅ Hitung total harga BERDASARKAN berat & harga dari master
            var totalHargaDecimal = hargaIkan.HargaIkan * beratKg;
            var totalHarga = (long)totalHargaDecimal;


            UpdatedModel = new IkanTangkapanModel
            {
                // primary key & relasi tetap
                kodetangkapan = _original.kodetangkapan,
                ID_User = _original.ID_User,
                KodeIkan = hargaIkan.KodeIkan,
                kode_ikan = hargaIkan.KodeIkan,

                // data lainnya
                TanggalInput = _original.TanggalInput,
                NamaIkan = namaIkan,
                Lokasi = txtLokasi.Text.Trim(),
                JamTangkap = jamTangkap,
                BeratKg = beratKg,
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
