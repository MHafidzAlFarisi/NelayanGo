using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
/*
namespace NelayanGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var inputWindow = new InputTangapanWindow();
            inputWindow.Show();
        }
    }
}
*/


using Npgsql;
using NelayanGo.DataServices;

namespace NelayanGo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestConnection();
        }

        public void TestConnection()
        {
            using var conn = new NpgsqlConnection(DatabaseConfig.ConnectionString);

            try
            {
                conn.Open();
                MessageBox.Show("Connected to Supabase!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed:\n{ex.Message}");
            }
        }
    }
}
