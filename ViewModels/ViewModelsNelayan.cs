//using NelayanGo.DataServices;
//using NelayanGo.Models;
//using System.ComponentModel; // Untuk INotifyPropertyChanged
//using System;
//using System.Collections.ObjectModel;

//namespace NelayanGo.ViewModels
//{
//    // Implement INotifyPropertyChanged untuk notifikasi perubahan ke UI
//    public class ViewModelsNelayan : INotifyPropertyChanged
//    {
//        private Nelayan? _currentNelayan;

//        // Properti yang di-bind di XAML
//        public Nelayan? CurrentNelayan
//        {
//            get => _currentNelayan;
//            set
//            {
//                _currentNelayan = value;
//                OnPropertyChanged(nameof(CurrentNelayan));
//            }
//        }

//        public ViewModelsNelayan()
//        {
//            // PENTING: Dalam aplikasi nyata, Anda akan mendapatkan kode ini dari sesi login
//            string initialKode = "NG-001"; // Contoh Kode Identik

//            DataServiceNelayan dataService = new DataServiceNelayan();
//            CurrentNelayan = dataService.GetDataNelayan(initialKode);

//            // Menetapkan data dummy jika koneksi database gagal atau data tidak ditemukan
//            if (CurrentNelayan == null)
//            {
//                CurrentNelayan = GetDummyNelayanData(initialKode);
//            }
//        }

//        // Metode untuk membuat data dummy jika koneksi database tidak berfungsi
//        private Nelayan GetDummyNelayanData(string kode)
//        {
//            return new Nelayan
//            {
//                Nama = "Ikan Setiawan (Dummy)",
//                KodeIdentik = kode,
//                Wilayah = "Pesisir Utara Jawa",
//                TahunBergabung = "2018",
//                Status = "Aktif",
//                NIK = "3216001234567890",
//                TanggalLahir = "12/03/1985",
//                TempatLahir = "Pangandaran",
//                NomorTelepon = "0812-3456-7890",
//                Email = "ikan.setiawan@nelayango.com",
//                Agama = "Islam",
//                GolonganDarah = "O",
//                AlamatKTP = "Desa Bahari, RT 01/RW 02, Pangandaran",

//                // Data Info
//                TotalWaktuAktifHari = 852,
//                TotalJarakBerlayarKM = 12450,
//                TotalBeratTangkapanTON = 45,
//                TotalJumlahBerlayar = 320,
//                TotalJamBerlayarJAM = 6500,
//                TotalJenisTangkapan = 15
//            };
//        }

//        // Boilerplate INotifyPropertyChanged
//        public event PropertyChangedEventHandler? PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}

using NelayanGo.DataServices;
using NelayanGo.Models;
using System.ComponentModel; // Diperlukan untuk INotifyPropertyChanged
using System;
using System.Collections.ObjectModel;

namespace NelayanGo.ViewModels
{
    // Implement INotifyPropertyChanged untuk notifikasi perubahan ke UI
    public class ViewModelsNelayan : INotifyPropertyChanged
    {
        private Nelayan? _currentNelayan;

        // Properti yang di-bind di XAML: CurrentNelayan
        public Nelayan? CurrentNelayan
        {
            get => _currentNelayan;
            set
            {
                _currentNelayan = value;
                OnPropertyChanged(nameof(CurrentNelayan));
            }
        }

        public ViewModelsNelayan()
        {
            // PENTING: Dalam aplikasi nyata, Anda akan mendapatkan kode ini dari sesi login
            string initialKode = "NG-001"; // Contoh Kode Identik

            DataServiceNelayan dataService = new DataServiceNelayan();
            CurrentNelayan = dataService.GetDataNelayan(initialKode);

            // Menetapkan data dummy jika koneksi database gagal atau data tidak ditemukan
            if (CurrentNelayan == null)
            {
                Console.WriteLine($"Menggunakan data dummy untuk nelayan: {initialKode}");
                CurrentNelayan = GetDummyNelayanData(initialKode);
            }
        }

        // Metode untuk membuat data dummy jika koneksi database tidak berfungsi
        private Nelayan GetDummyNelayanData(string kode)
        {
            return new Nelayan
            {
                Nama = "Ikan Setiawan (Dummy)",
                KodeIdentik = kode,
                Wilayah = "Pesisir Utara Jawa",
                TahunBergabung = "2018",
                Status = "Aktif",
                NIK = "3216001234567890",
                TanggalLahir = "12 Maret 1985",
                TempatLahir = "Pangandaran",
                NomorTelepon = "0812-3456-7890",
                Email = "ikan.setiawan@nelayango.com",
                Agama = "Islam",
                GolonganDarah = "O",
                AlamatKTP = "Desa Bahari, RT 01/RW 02, Pangandaran, Jawa Barat",

                // Data Info
                TotalWaktuAktifHari = 852,
                TotalJarakBerlayarKM = 12450,
                TotalBeratTangkapanTON = 45,
                TotalJumlahBerlayar = 320,
                TotalJamBerlayarJAM = 6500,
                TotalJenisTangkapan = 15
            };
        }

        // Boilerplate INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}