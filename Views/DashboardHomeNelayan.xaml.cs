using System.Windows;

namespace NelayanGo.Views
{
    public partial class DashboardHomeNelayan : Window
    {
        public DashboardHomeNelayan()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
