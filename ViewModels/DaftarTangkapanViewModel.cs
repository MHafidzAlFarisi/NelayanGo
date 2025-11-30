using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NelayanGo.DataServices;
using NelayanGo.Models;

namespace NelayanGo.ViewModels
{
    public class DaftarTangkapanViewModel : INotifyPropertyChanged
    {
        private readonly IkanTangkapanDataService _service = new();
        private readonly NelayanDataService _profilService = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(nameof(CurrentNelayan)); }
        }

        // Ambil ID dari sesi login
        public long CurrentUserId
        {
            get
            {
                if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out long id))
                    return id;
                return 1; // Default fallback
            }
        }

        private ObservableCollection<IkanTangkapanModel> _daftarTangkapan =
            new ObservableCollection<IkanTangkapanModel>();
        public ObservableCollection<IkanTangkapanModel> DaftarTangkapan
        {
            get => _daftarTangkapan;
            set
            {
                _daftarTangkapan = value;
                OnPropertyChanged(nameof(DaftarTangkapan));
            }
        }

        private IkanTangkapanModel? _selectedTangkapan;
        public IkanTangkapanModel? SelectedTangkapan
        {
            get => _selectedTangkapan;
            set
            {
                _selectedTangkapan = value;
                OnPropertyChanged(nameof(SelectedTangkapan));
            }
        }

        public DaftarTangkapanViewModel()
        {
            LoadUserProfile(); // Load header profile
            LoadData();
        }

        private async void LoadUserProfile()
        {
            if (AppSession.CurrentUser != null)
            {
                string username = AppSession.CurrentUser.Username;
                var profil = await _profilService.GetProfilByUserId(CurrentUserId);

                if (profil != null)
                    CurrentNelayan = profil;
                else
                    CurrentNelayan = new NelayanModel { Nama = username, KodeIdentik = "Belum Input Data" };
            }
            else
            {
                CurrentNelayan = new NelayanModel { Nama = "Tamu", KodeIdentik = "---" };
            }
        }

        public void LoadData()
        {
            DaftarTangkapan.Clear();
            try
            {
                var data = _service.GetByUser(CurrentUserId);
                foreach (var item in data)
                    DaftarTangkapan.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal load data tangkapan:\n{ex.Message}");
            }
        }

        public void Add(IkanTangkapanModel model)
        {
            try
            {
                model.ID_User = CurrentUserId;
                model.TanggalInput = DateTime.UtcNow;
                _service.Insert(model);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambah data tangkapan:\n{ex.Message}");
            }
        }

        public void Update(IkanTangkapanModel model)
        {
            try
            {
                model.ID_User = CurrentUserId;
                _service.Update(model);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengubah data tangkapan:\n{ex.Message}");
            }
        }

        public void DeleteSelected()
        {
            if (SelectedTangkapan == null)
            {
                MessageBox.Show("Pilih data tangkapan yang ingin dihapus.");
                return;
            }

            try
            {
                _service.Delete(SelectedTangkapan.kodetangkapan, CurrentUserId);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menghapus data tangkapan:\n{ex.Message}");
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}