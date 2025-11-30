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

        public event PropertyChangedEventHandler? PropertyChanged;

        public long CurrentUserId { get; } = AppSession.CurrentUser?.Id ?? 0;

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
            LoadData();
        }

        // === LOAD DATA, HANYA UNTUK USER INI ===
        public void LoadData()
        {
            DaftarTangkapan.Clear();

            try
            {
                // sementara pakai user dummy
                var data = _service.GetByUser(CurrentUserId);

                foreach (var item in data)
                    DaftarTangkapan.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal load data tangkapan:\n{ex.Message}");
            }
        }

        // === CREATE ===
        public void Add(IkanTangkapanModel model)
        {
            try
            {
                // pastikan selalu milik user saat ini
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

        // === UPDATE ===
        public void Update(IkanTangkapanModel model)
        {
            try
            {
                // pastikan ID_User tetap user ini
                model.ID_User = CurrentUserId;

                _service.Update(model);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengubah data tangkapan:\n{ex.Message}");
            }
        }

        // === DELETE ===
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
