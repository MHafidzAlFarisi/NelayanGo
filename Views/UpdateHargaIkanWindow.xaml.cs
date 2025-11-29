using System;
using System.Globalization;
using System.Windows;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.Views
{
    public partial class UpdateHargaIkanWindow : Window
    {
        private readonly HargaIkanDataService _service = new();
        private readonly HargaIkanModel _model;

        public UpdateHargaIkanWindow(HargaIkanModel model)
        {
            InitializeComponent();
            _model = model;

            // Prefill
            txtNamaIkan.Text = _model.NamaIkan;
            txtHargaIkan.Text = _model.HargaIkan.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaIkan.Text) ||
                string.IsNullOrWhiteSpace(txtHargaIkan.Text))
            {
                MessageBox.Show("Nama ikan, harga, dan wilayah wajib diisi.");
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

            _model.NamaIkan = txtNamaIkan.Text.Trim();
            _model.HargaIkan = harga;
            _model.Wilayah = "";
            _model.TanggalUpdate = DateTime.UtcNow;

            try
            {
                _service.Update(_model);
                MessageBox.Show("Data harga ikan berhasil diperbarui.");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengubah data:\n{ex.Message}");
            }
        }

        private void BtnBatal_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
