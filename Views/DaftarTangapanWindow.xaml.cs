using System.Windows;

namespace NelayanGo
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
    }
}