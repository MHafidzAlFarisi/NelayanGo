using NelayanGo.Models;
using NelayanGo.DataServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace NelayanGo.ViewModels
{
    public class ViewModelsNelayan : INotifyPropertyChanged
    {
        private NelayanModel? _currentNelayan;
        private readonly NelayanDataService _service = new();

        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        public ViewModelsNelayan()
        {
            // Panggil fungsi Load Data secara async
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            // Ambil ID User yang sedang login
            // Pastikan Anda sudah menyimpan user login di AppSession.CurrentUser
            if (AppSession.CurrentUser != null)
            {
                // Parse ID string ke long
                if (long.TryParse(AppSession.CurrentUser.Id, out long userId))
                {
                    var dataAsli = await _service.GetProfilByUserId(userId);

                    if (dataAsli != null)
                    {
                        CurrentNelayan = dataAsli; // TAMPILKAN DATA ASLI
                        return; // Keluar, jangan load dummy
                    }
                }
            }

            // Jika User belum login atau Data tidak ditemukan di DB, baru tampilkan Dummy
            CurrentNelayan = GetDummyNelayanData();
        }

        private NelayanModel GetDummyNelayanData()
        {
            return new NelayanModel
            {
                Nama = "Ikan Setiawan (Dummy)",
                KodeIdentik = "NG-001",
                Wilayah = "Pesisir Utara Jawa",
                TahunBergabung = "2018",
                Status = "Aktif",
                NIK = "Data Belum Ada",
                TanggalLahir = "-",
                TempatLahir = "-",
                NomorTelepon = "-",
                Email = "-",
                Agama = "-",
                GolonganDarah = "-",
                AlamatKTP = "Silakan input data profil Anda",
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}