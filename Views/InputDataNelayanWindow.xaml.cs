using System.Windows;
using NelayanGo.Models;
using NelayanGo.DataServices;
using System;

namespace NelayanGo.Views
{
    public partial class InputDataNelayanWindow : Window
    {
        private readonly NelayanDataService _nelayanService = new();
        private long _currentUserId;

        public InputDataNelayanWindow()
        {
            InitializeComponent();
            _currentUserId = 1;
        }

        public InputDataNelayanWindow(string userIdString)
        {
            InitializeComponent();
            if (long.TryParse(userIdString, out long result))
            {
                _currentUserId = result;
            }
            else
            {
                _currentUserId = 1;
                MessageBox.Show("Error membaca ID User, menggunakan ID default.");
            }
        }

        public NelayanModel? ResultNelayan { get; private set; }

        private async void SimpanButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtNIK.Text))
            {
                MessageBox.Show("Mohon lengkapi data utama (Nama dan NIK).", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var btn = sender as System.Windows.Controls.Button;
            if (btn != null) btn.IsEnabled = false;

            try
            {
                var kodeOtomatis = $"NLY-{DateTime.Now:yyMMddHHmmss}";

                var dataBaru = new NelayanModel
                {
                    UserId = _currentUserId,
                    Nama = txtNama.Text.Trim(),
                    KodeIdentik = kodeOtomatis,
                    TahunBergabung = DateTime.Now.Year.ToString(),
                    Status = "Aktif",
                    Email = "-",
                    Wilayah = txtWilayah.Text.Trim(),
                    NIK = txtNIK.Text.Trim(),
                    TanggalLahir = dpTanggalLahir.SelectedDate?.ToString("yyyy-MM-dd") ?? null,
                    TempatLahir = txtTempatLahir.Text.Trim(),
                    AlamatKTP = txtAlamat.Text.Trim(),
                    NomorTelepon = txtNoTelp.Text.Trim(),
                    Agama = txtAgama.Text.Trim(),
                    GolonganDarah = cmbGoldar.Text
                };

                bool sukses = await _nelayanService.InsertProfilNelayan(dataBaru);

                if (sukses)
                {
                    ResultNelayan = dataBaru;

                    // --- UBAH NAVIGASI KE LOGIN ---
                    MessageBox.Show("Data profil berhasil disimpan! Silakan Login kembali untuk masuk.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

                    var loginWindow = new LoginWindow();
                    loginWindow.Show();

                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
            finally
            {
                if (btn != null) btn.IsEnabled = true;
            }
        }

        private void BatalButton_Click(object sender, RoutedEventArgs e)
        {
            // Jika batal, kembalikan ke Login juga agar tidak nyangkut
            var login = new LoginWindow();
            login.Show();

            DialogResult = false;
            Close();
        }
    }
}