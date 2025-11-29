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
using System.Windows;
using NelayanGo.ViewModels;

namespace NelayanGo
{
    public partial class AdminHargaIkanWindow : Window
    {
        private AdminHargaIkanViewModel Vm => DataContext as AdminHargaIkanViewModel;

        public AdminHargaIkanWindow()
        {
            InitializeComponent();
        }

        private void InputHargaButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new InputHargaIkanWindow() { Owner = this };

            if (win.ShowDialog() == true)
            {
                Vm.AddHargaBaru(win.NamaIkan, win.HargaIkan);
            }
        }

        private void UpdateHargaButton_Click(object sender, RoutedEventArgs e)
        {
            if (Vm.SelectedHarga == null)
            {
                MessageBox.Show("Pilih data yang mau diupdate.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var sel = Vm.SelectedHarga;

            var win = new UpdateHargaIkanWindow(
                sel.KodeIkan, sel.NamaIkan, sel.HargaIkan)
            {
                Owner = this
            };

            if (win.ShowDialog() == true)
            {
                Vm.UpdateHarga(win.KodeIkan, win.NamaIkanBaru, win.HargaIkanBaru);
            }
        }

        private void DeleteHargaButton_Click(object sender, RoutedEventArgs e)
        {
            if (Vm.SelectedHarga == null)
            {
                MessageBox.Show("Pilih data yang mau dihapus.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (MessageBox.Show(
                    "Yakin ingin menghapus harga ikan ini?",
                    "Konfirmasi",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Vm.DeleteHarga(Vm.SelectedHarga.KodeIkan);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // sementara kosong, nanti bisa aku bikinin filternya
        }
    }

}

