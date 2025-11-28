using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;

namespace NelayanGo.ViewModels
{
    public class DaftarTangapanViewModel : INotifyPropertyChanged
    {
        // ObservableCollection untuk menyimpan daftar tangkapan yang dapat di-bind ke DataGrid
        public ObservableCollection<CatchModel> TangkapanList { get; set; }

        public DaftarTangapanViewModel()
        {
            TangkapanList = new ObservableCollection<CatchModel>();
            LoadDummyData(); // Muat data dummy saat ViewModel dibuat
        }

        // Metode untuk mengisi data dummy sesuai gambar
        private void LoadDummyData()
        {
            var today = "01-02-2025";

            // Data dummy berdasarkan gambar yang Anda berikan
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0001-020225", JamTangkap = "19:00-20:00", TanggalTangkap = today, NamaIkan = "Tongkol", BeratKg = 1.25, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.00" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0002-020225", JamTangkap = "19:00-20:00", TanggalTangkap = today, NamaIkan = "Kakap", BeratKg = 1.33, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.00" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0003-020225", JamTangkap = "19:00-20:00", TanggalTangkap = today, NamaIkan = "Bandeng", BeratKg = 1.75, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.00" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0004-020225", JamTangkap = "19:00-20:00", TanggalTangkap = today, NamaIkan = "Tenggiri", BeratKg = 1.54, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.00" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0005-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Kerapu", BeratKg = 5.45, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.01" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0006-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Teri", BeratKg = 5.32, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.01" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0007-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Bawal", BeratKg = 6.34, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.01" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0008-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Kerapu", BeratKg = 4.32, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.01" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0009-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Bandeng", BeratKg = 3.33, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.02" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0010-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Layang", BeratKg = 4.45, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.02" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0011-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Kakap", BeratKg = 6.45, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.02" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0012-020225", JamTangkap = "20:00-21:00", TanggalTangkap = today, NamaIkan = "Tongkol", BeratKg = 4.85, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.02" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0013-020225", JamTangkap = "21:00-22:00", TanggalTangkap = today, NamaIkan = "Kerapu", BeratKg = 2.65, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.03" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0014-020225", JamTangkap = "21:00-22:00", TanggalTangkap = today, NamaIkan = "Tenggiri", BeratKg = 3.45, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.03" });
            TangkapanList.Add(new CatchModel { ID = "TGL-32-0015-020225", JamTangkap = "21:00-22:00", TanggalTangkap = today, NamaIkan = "Bawal", BeratKg = 4.42, Lokasi = "-8.033341638731791, 110.18654457227322", JamInput = "13.03" });
        }

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}