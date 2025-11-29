using System;
using System.Globalization;
using System.Windows;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.Views
{
    public partial class InputHargaIkanWindow : Window
    {
        private readonly HargaIkanDataService _service = new();
        private readonly long _adminId;
        private readonly string _adminWilayah;

        public InputHargaIkanWindow(long adminId, string adminWilayah)
        {
            InitializeComponent();
            _adminId = adminId;
            _adminWilayah = adminWilayah ?? string.Empty;
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaIkan.Text) ||
                string.IsNullOrWhiteSpace(txtHargaIkan.Text))
            {
                MessageBox.Show("Nama ikan dan harga wajib diisi.");
                return;
            }

            if (!decimal.TryParse(
                    txtHargaIkan.Text,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var harga))
            {
                MessageBox.Show("Format harga tidak valid.");
                return;
            }

            var model = new HargaIkanModel
            {
                NamaIkan = txtNamaIkan.Text.Trim(),
                HargaIkan = harga,
                Wilayah = _adminWilayah,
                TanggalUpdate = DateTime.UtcNow,
                ID_Admin = _adminId
            };

            try
            {
                _service.Insert(model);
                MessageBox.Show("Data harga ikan berhasil ditambahkan.");
                DialogResult = true; // supaya AdminHargaIkanWindow tahu harus reload
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan data:\n{ex.Message}");
            }
        }

        private void BtnBatal_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}