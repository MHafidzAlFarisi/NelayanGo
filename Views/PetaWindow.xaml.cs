using System.Windows;
using NelayanGo.Helpers;
using System.Windows.Input;
using NelayanGo.ViewModels;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace NelayanGo.Views
{
    public partial class PetaWindow : Window
    {
        private PetaCuacaViewModel _vm;
        private GMapMarker _startMarker;
        private GMapMarker _endMarker;

        public PetaWindow()
        {
            InitializeComponent();
            // Event Loaded untuk inisialisasi peta aman
            this.Loaded += PetaWindow_Loaded;
        }

        private void PetaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = (PetaCuacaViewModel)this.DataContext;

            // 1. Konfigurasi GMap
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MainMap.MapProvider = GMapProviders.GoogleMap; // Atau OpenStreetMap
            MainMap.ShowCenter = false; // Hilangkan crosshair tengah
            MainMap.MinZoom = 5;
            MainMap.MaxZoom = 18;
            MainMap.Zoom = 10;

            // Set posisi awal kamera
            MainMap.Position = new PointLatLng(_vm.StartLat, _vm.StartLng);

            // 2. Buat Marker
            _startMarker = CreateMarker(Brushes.Blue, "Awal");
            _endMarker = CreateMarker(Brushes.Red, "Tujuan");

            MainMap.Markers.Add(_startMarker);
            MainMap.Markers.Add(_endMarker);

            // Update posisi marker pertama kali
            UpdateMarkerPositions();
        }

        private GMapMarker CreateMarker(Brush color, string tooltip)
        {
            var marker = new GMapMarker(new PointLatLng(0, 0));
            // Bentuk Marker (Lingkaran)
            var shape = new Ellipse
            {
                Width = 20,
                Height = 20,
                Stroke = Brushes.White,
                StrokeThickness = 2,
                Fill = color,
                ToolTip = tooltip
            };
            marker.Shape = shape;
            // Geser offset agar ujung marker pas di titik (tengah lingkaran)
            marker.Offset = new Point(-10, -10);
            return marker;
        }

        // Event Double Click pada Peta untuk pilih lokasi
        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ambil koordinat dari posisi mouse
            Point point = e.GetPosition(MainMap);
            PointLatLng latlng = MainMap.FromLocalToLatLng((int)point.X, (int)point.Y);

            // Update ViewModel (ini akan memicu PropertyChanged)
            _vm.SetLocationFromMap(latlng.Lat, latlng.Lng);

            // PENTING: Update visual marker di peta
            UpdateMarkerPositions();
        }

        // Event saat text koordinat berubah (Ketik Manual)
        private void Coordinate_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_vm == null || MainMap == null) return;

            UpdateMarkerPositions();

            // Pindahkan kamera ke titik yang baru diedit
            if (_vm.SelectionMode == 0)
                MainMap.Position = new PointLatLng(_vm.StartLat, _vm.StartLng);
            else
                MainMap.Position = new PointLatLng(_vm.EndLat, _vm.EndLng);
        }

        private void UpdateMarkerPositions()
        {
            if (_startMarker != null)
                _startMarker.Position = new PointLatLng(_vm.StartLat, _vm.StartLng);

            if (_endMarker != null)
                _endMarker.Position = new PointLatLng(_vm.EndLat, _vm.EndLng);
        }

        // Logic Radio Button
        private void Radio_Checked_Awal(object sender, RoutedEventArgs e) { if (_vm != null) _vm.SelectionMode = 0; }
        private void Radio_Checked_Tujuan(object sender, RoutedEventArgs e) { if (_vm != null) _vm.SelectionMode = 1; }

        private void BtnCurrentLoc_Click(object sender, RoutedEventArgs e)
        {
            // Panggil command di ViewModel
            if (_vm.PilihLokasiCommand.CanExecute(null))
                _vm.PilihLokasiCommand.Execute(null);

            UpdateMarkerPositions();
            MainMap.Position = new PointLatLng(_vm.StartLat, _vm.StartLng);
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