using System.Windows;
using NelayanGo.Helpers;
using System.Windows.Input;
using NelayanGo.ViewModels;

namespace NelayanGo.Views
{
    /// <summary>
    /// Interaction logic for PetaWindow.xaml
    /// </summary>
    // Pastikan x:Class di XAML cocok dengan nama kelas ini
    public partial class PetaWindow : Window
    {
        public PetaWindow()
        {
            // InitializeComponent() akan ditemukan setelah XAML (PetaWindow.xaml)
            // berhasil dikompilasi tanpa error styling (Padding/CornerRadius pada StackPanel).
            InitializeComponent();
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