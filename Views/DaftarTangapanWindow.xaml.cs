using NelayanGo.Helpers;
using System.Windows;
using System.Windows.Input;

namespace NelayanGo.Views
{
    /// <summary>
    /// Interaction logic for DaftarTangapanWindow.xaml
    /// </summary>
    public partial class DaftarTangapanWindow : Window
    {
        public DaftarTangapanWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Menangani klik tombol "INPUT TANGKAPAN" dan membuka jendela input.
        /// </summary>
        private void InputTangapanButton_Click(object sender, RoutedEventArgs e)
        {
            // Pastikan Anda telah membuat InputTangapanWindow di proyek Anda
            var inputWindow = new InputTangapanWindow();
            inputWindow.Show();
            // Opsional: Sembunyikan jendela daftar ini
            // this.Hide();
        }
        private void HomeLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Cukup panggil helper, dan berikan tujuan sebagai string
            NavigationHelper.NavigateFromHeaderClick(sender, "Home");
        }

        private void HasilTangkapanLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Cukup panggil helper, dan berikan tujuan sebagai string
            NavigationHelper.NavigateFromHeaderClick(sender, "Daftar Tangkapan");
        }

        // Tambahkan handler untuk navigasi lain
        private void MapsLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Maps");
        }
        // Tambahkan handler untuk navigasi lain
        private void HargaPasarLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateFromHeaderClick(sender, "Harga pasar");
        }
    }
}