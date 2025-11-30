using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using NelayanGo.Models;
using NelayanGo.ViewModels;

namespace NelayanGo.Views
{
    public partial class DaftarTangkapanWindow : Window
    {
        private DaftarTangkapanViewModel _vm;
        private ICollectionView _view;

        public DaftarTangkapanWindow()
        {
            InitializeComponent();

            _vm = DataContext as DaftarTangkapanViewModel
                  ?? throw new System.InvalidOperationException("DataContext harus DaftarTangkapanViewModel");

            _view = CollectionViewSource.GetDefaultView(_vm.DaftarTangkapan);
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = SearchTextBox.Text?.Trim().ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(text))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = item =>
                {
                    if (item is not IkanTangkapanModel t) return false;

                    return (t.NamaIkan?.ToLower().Contains(text) ?? false)
                           || (t.Lokasi?.ToLower().Contains(text) ?? false)
                           || t.kodetangkapan.ToString().Contains(text);
                };
            }

            _view.Refresh();
        }

        private void InputTangkapanButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new InputTangkapanWindow();
            var result = win.ShowDialog();

            if (result == true && win.ResultModel != null)
            {
                _vm.Add(win.ResultModel);
                _view.Refresh();
            }
        }

        private void UpdateTangkapanButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedTangkapan == null)
            {
                MessageBox.Show("Pilih data yang ingin diupdate.");
                return;
            }

            var win = new UpdateTangkapanWindow(_vm.SelectedTangkapan);
            var result = win.ShowDialog();

            if (result == true && win.UpdatedModel != null)
            {
                _vm.Update(win.UpdatedModel);
                _view.Refresh();
            }
        }

        private void DeleteTangkapanButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedTangkapan == null)
            {
                MessageBox.Show("Pilih data yang ingin dihapus.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Yakin ingin menghapus tangkapan {_vm.SelectedTangkapan.NamaIkan}?",
                "Konfirmasi Hapus",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            _vm.DeleteSelected();
            _view.Refresh();
        }

        private void dgTangkapan_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}