using System.Windows;

namespace NelayanGo.Views
{
    public partial class DashboardHomeAdmin : Window
    {
        public DashboardHomeAdmin()
        {
            InitializeComponent();
        }

        //private void OpenAdminHargaIkan_Click(object sender, RoutedEventArgs e)
        //{
        //    // Kalau kamu mau langsung buka halaman AdminHargaIkan
        //    var win = new AdminHargaIkanWindow();
        //    win.Show();
        //}

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
