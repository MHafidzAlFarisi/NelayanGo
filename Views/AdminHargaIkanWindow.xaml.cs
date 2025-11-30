using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using NelayanGo.Models;
using NelayanGo.ViewModels;

namespace NelayanGo.Views
{
    public partial class AdminHargaIkanWindow : Window
    {
        private AdminHargaIkanViewModel _vm;
        private ICollectionView _view;

        private readonly long _currentAdminId = 1;      // TODO: nanti dari auth
        private readonly string _currentAdminWilayah = "Bantul"; // TODO: nanti dari admin login

        public AdminHargaIkanWindow()
        {
            InitializeComponent();

            _vm = DataContext as AdminHargaIkanViewModel 
                  ?? throw new InvalidOperationException("DataContext harus AdminHargaIkanViewModel");

            // View untuk search/filter
            _view = CollectionViewSource.GetDefaultView(_vm.DaftarHarga);
        }

        private void InputHargaButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Views.InputHargaIkanWindow(_currentAdminId, _currentAdminWilayah);
            var result = win.ShowDialog();

            if (result == true)
            {
                _vm.LoadData();
                _view.Refresh();
            }
        }

        private void UpdateHargaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedHarga == null)
            {
                MessageBox.Show("Pilih data yang ingin diupdate dulu.");
                return;
            }

            var win = new Views.UpdateHargaIkanWindow(_vm.SelectedHarga);
            var result = win.ShowDialog();

            if (result == true)
            {
                _vm.LoadData();
                _view.Refresh();
            }
        }

        private void DeleteHargaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedHarga == null)
            {
                MessageBox.Show("Pilih data yang ingin dihapus dulu.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Yakin ingin menghapus { _vm.SelectedHarga.NamaIkan }?",
                "Konfirmasi Hapus",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            _vm.DeleteHarga(_vm.SelectedHarga.KodeIkan);
            _view.Refresh();
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = SearchTextBox.Text?.Trim().ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(text))
            {
                _view.Filter = null;  // tampiikan semua
            }
            else
            {
                _view.Filter = item =>
                {
                    if (item is not HargaIkanModel h) return false;

                    return (h.NamaIkan?.ToLower().Contains(text) ?? false)
                           || (h.Wilayah?.ToLower().Contains(text) ?? false)
                           || h.KodeIkan.ToString().Contains(text);
                };
            }

            _view.Refresh();
        }

        private void LogOutLink_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var result = MessageBox.Show(
                "Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
                return;

            AppSession.CurrentUser = null;

            var loginWindow = new LoginWindow();
            loginWindow.Show();

            Application.Current.Windows
                .OfType<Window>()
                .Where(w => w != loginWindow)
                .ToList()
                .ForEach(w => w.Close());
        }

    }
}
