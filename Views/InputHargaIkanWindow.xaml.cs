using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Globalization;

namespace NelayanGo
{
    public partial class InputHargaIkanWindow : Window
    {
        // --- hasil yang bisa dibaca dari luar setelah ShowDialog() ---
        public string NamaIkan { get; private set; } = string.Empty;
        public decimal HargaPerKg { get; private set; }

        public InputHargaIkanWindow()
        {
            InitializeComponent();
        }

        // Tombol BATAL
        private void BatalButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;   // menandakan user batal
            Close();
        }

        // Tombol KIRIM
        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            var nama = NamaIkanTextBox.Text.Trim();
            var hargaText = HargaTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nama))
            {
                MessageBox.Show("Nama ikan tidak boleh kosong.",
                                "Validasi",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                NamaIkanTextBox.Focus();
                return;
            }

            if (!decimal.TryParse(
                    hargaText,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var harga))
            {
                MessageBox.Show("Harga ikan harus berupa angka (contoh: 15000 atau 15000.50).",
                                "Validasi",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                HargaTextBox.Focus();
                return;
            }

            if (harga <= 0)
            {
                MessageBox.Show("Harga ikan harus lebih dari 0.",
                                "Validasi",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                HargaTextBox.Focus();
                return;
            }

            // simpan ke properti hasil
            NamaIkan = nama;
            HargaPerKg = harga;

            DialogResult = true;    // sukses submit
            Close();
        }
    }
}

