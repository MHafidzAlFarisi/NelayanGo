using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NelayanGo.Models;
using NelayanGo.DataServices;

namespace NelayanGo.ViewModels
{
    public class AdminProfileViewModel : INotifyPropertyChanged
    {
        private readonly NelayanDataService _profilService = new();

        // Expose CurrentNelayan for bindings in AdminProfileWindow.xaml
        private NelayanModel? _currentNelayan;
        public NelayanModel? CurrentNelayan
        {
            get => _currentNelayan;
            set { _currentNelayan = value; OnPropertyChanged(); }
        }

        public AdminProfileViewModel()
        {
            // Try load real profile; fallback to dummy if not available.
            LoadUserProfile();
        }

        private async void LoadUserProfile()
        {
            // If there is a logged-in user, try to fetch profile from service
            if (AppSession.CurrentUser != null && long.TryParse(AppSession.CurrentUser.Id, out long userId))
            {
                try
                {
                    var profil = await _profilService.GetProfilByUserId(userId);
                    if (profil != null)
                    {
                        CurrentNelayan = profil;
                        return;
                    }
                }
                catch
                {
                    // Ignore and fall back to dummy below
                }

                // fallback when service returned null
                CurrentNelayan = new NelayanModel
                {
                    Nama = AppSession.CurrentUser.Username,
                    Wilayah = "Belum Input Wilayah",
                    Status = "Unknown",
                    NIK = "-",
                    TanggalLahir = "-",
                    TempatLahir = "-",
                    AlamatKTP = "-",
                    NomorTelepon = "-",
                    Agama = "-",
                    GolonganDarah = "-",
                    KodeIdentik = "Belum Input Data"
                };
            }
            else
            {
                // No logged-in user: show guest/dummy profile
                CurrentNelayan = new NelayanModel
                {
                    Nama = "Tamu",
                    KodeIdentik = "---",
                    Wilayah = "-",
                    Status = "-",
                    NIK = "-",
                    TanggalLahir = "-",
                    TempatLahir = "-",
                    AlamatKTP = "-",
                    NomorTelepon = "-",
                    Agama = "-",
                    GolonganDarah = "-"
                };
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}