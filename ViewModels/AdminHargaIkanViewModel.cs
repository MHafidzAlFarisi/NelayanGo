using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.ViewModels
{
    public class AdminHargaIkanViewModel : INotifyPropertyChanged
    {
        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        private readonly HargaIkanDataService _service = new();
        private readonly NelayanDataService _profilService = new(); // <-- added

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<HargaIkanModel> _daftarHarga;
        public ObservableCollection<HargaIkanModel> DaftarHarga
        {
            get => _daftarHarga;
            set
            {
                _daftarHarga = value;
                OnPropertyChanged(nameof(DaftarHarga));
            }
        }

        private HargaIkanModel _selectedHarga;
        public HargaIkanModel SelectedHarga
        {
            get => _selectedHarga;
            set
            {
                _selectedHarga = value;
                OnPropertyChanged(nameof(SelectedHarga));
            }
        }

        public AdminHargaIkanViewModel()
        {
            DaftarHarga = new ObservableCollection<HargaIkanModel>();

            LoadUserProfile();

            LoadData();
        }

        private async void LoadUserProfile()
        {
            if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out var userId))
            {
                var profil = await _profilService.GetProfilByUserId(userId);
                CurrentNelayan = profil ?? new NelayanModel { Nama = AppSession.CurrentUser.Username, KodeIdentik = "Belum Input Data" };
            }
            else
            {
                CurrentNelayan = new NelayanModel { Nama = "Tamu", KodeIdentik = "---" };
            }
        }

        public void LoadData()
        {
            DaftarHarga.Clear();

            try
            {
                var data = _service.GetAll();
                foreach (var item in data)
                    DaftarHarga.Add(item);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Gagal load data harga ikan:\n{ex.Message}");
            }
        }

        public void AddHarga(HargaIkanModel modelBaru)
        {
            try
            {
                _service.Insert(modelBaru);
                LoadData();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Gagal menambah data:\n{ex.Message}");
            }
        }

        public void UpdateHarga(HargaIkanModel modelUpdate)
        {
            try
            {
                _service.Update(modelUpdate);
                LoadData();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Gagal mengubah data:\n{ex.Message}");
            }
        }

        public void DeleteHarga(long kodeIkan)
        {
            try
            {
                _service.Delete(kodeIkan);
                LoadData();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Gagal menghapus data:\n{ex.Message}");
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}