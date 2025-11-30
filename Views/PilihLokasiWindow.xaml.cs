using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace NelayanGo.Views
{
    public partial class PilihLokasiWindow : Window
    {
        public double SelectedLat { get; private set; }
        public double SelectedLng { get; private set; }
        public string SelectedCoordinateString => $"{SelectedLat:F6}, {SelectedLng:F6}";

        private GMapMarker _currentMarker;

        public PilihLokasiWindow()
        {
            InitializeComponent();
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MapControl.MapProvider = GMapProviders.GoogleMap; 
            MapControl.MinZoom = 2;
            MapControl.MaxZoom = 18;
            MapControl.Zoom = 10;
            MapControl.ShowCenter = false;
            MapControl.DragButton = MouseButton.Left; 
            MapControl.Position = new PointLatLng(-7.765752, 110.371722);
        }

        private void MapControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(MapControl);
            PointLatLng latlng = MapControl.FromLocalToLatLng((int)point.X, (int)point.Y);

            SelectedLat = latlng.Lat;
            SelectedLng = latlng.Lng;

            UpdateMarker(latlng);

            txtKoordinatInfo.Text = $"Terpilih: {SelectedLat:F4}, {SelectedLng:F4}";
        }

        private void UpdateMarker(PointLatLng point)
        {
            MapControl.Markers.Clear();

            _currentMarker = new GMapMarker(point);

            var shape = new Ellipse
            {
                Width = 20,
                Height = 20,
                Stroke = Brushes.White,
                StrokeThickness = 2,
                Fill = Brushes.Red
            };

            _currentMarker.Shape = shape;
            _currentMarker.Offset = new Point(-10, -10); 

            MapControl.Markers.Add(_currentMarker);
        }

        private void BtnPilih_Click(object sender, RoutedEventArgs e)
        {
            if (_currentMarker == null)
            {
                MessageBox.Show("Silakan klik ganda pada peta untuk menandai lokasi terlebih dahulu.");
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}