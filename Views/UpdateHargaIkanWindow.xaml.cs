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
    public partial class UpdateHargaIkanWindow : Window
    {
        // misalnya kamu mau tahu ID record yang di-edit
        public string IdHarga { get; }

        public string NamaIkanBaru { get; private set; } = string.Empty;
        public decimal HargaBaruPerKg { get; private set; }

        // ctor default kalau mau dipakai bebas
        public UpdateHargaIkanWindow()
        {
            InitializeComponent();
        }

        // ctor dengan data awal (paling sering dipakai)
        public UpdateHargaIkanWindow(string id, string namaAwal, decimal hargaAwal)
            : this()
        {
            IdHarga = id;
            NamaIkanTextBox.Text = namaAwal;
            HargaTextBox.Text = hargaAwal.ToString(CultureInfo.InvariantCulture);
        }

        private void BatalButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

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
                MessageBox.Show("Harga ikan harus berupa angka.",
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

            NamaIkanBaru = nama;
            HargaBaruPerKg = harga;

            DialogResult = true;
            Close();
        }
    }
}

