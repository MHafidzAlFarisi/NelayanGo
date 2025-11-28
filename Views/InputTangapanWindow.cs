using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading; // Diperlukan untuk DispatcherTimer

namespace NelayanGo
{
    /// <summary>
    /// Interaction logic for InputTangapanWindow.xaml
    /// </summary>
    public partial class InputTangapanWindow : Window
    {
        private readonly DispatcherTimer _timer;

        public InputTangapanWindow()
        {
            // PENTING: InitializeComponent() harus dipanggil pertama kali di konstruktor
            InitializeComponent();

            // 1. Setup Timer untuk Jam Real-time
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Update setiap 1 detik
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // Panggil tick pertama kali untuk mengisi nilai awal
            Timer_Tick(null, EventArgs.Empty);
        }

        // Metode yang dipanggil setiap detik oleh timer
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // TimeTextBlock dan DateTextBlock sekarang dikenali karena ada x:Name di XAML

            // Update Jam (Format H:mm:ss)
            TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");

            // Update Tanggal (Format dd-MM-yyyy)
            DateTextBlock.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        // 2. Penanganan Tombol KIRIM
        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder: Logika kirim data ke database
            MessageBox.Show("Data Tangkapan siap untuk dikirim!", "Konfirmasi Kirim", MessageBoxButton.OK, MessageBoxImage.Information);

            // Dalam aplikasi nyata, Anda akan mengambil nilai dari TextBox,
            // memvalidasinya, dan memanggil DataService untuk menyimpannya.
        }

        // 3. Penanganan Tombol BATAL
        private void BatalButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder: Logika untuk membatalkan input (misalnya, menutup jendela)
            // Tentu, di aplikasi nyata Anda harus meminta konfirmasi dulu
            MessageBoxResult result = MessageBox.Show("Apakah Anda yakin ingin membatalkan input?", "Konfirmasi Pembatalan", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Pastikan timer dihentikan saat jendela ditutup untuk menghindari kebocoran memori
        protected override void OnClosed(EventArgs e)
        {
            _timer.Stop();
            base.OnClosed(e);
        }
    }
}