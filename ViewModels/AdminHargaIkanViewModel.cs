using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NelayanGo.DataServices;
using NelayanGo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NelayanGo.ViewModels
{
    public class AdminHargaIkanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HargalkanModel> DaftarHarga { get; set; }
        public HargalkanModel SelectedHarga { get; set; }

        public AdminHargaIkanViewModel()
        {
            DaftarHarga = new ObservableCollection<HargalkanModel>()
            {
                new HargalkanModel()
                {
                    Id = "ADM-00-0001",
                    NamaIkan = "Tongkol",
                    HargaPerKilo = 15000,
                    Wilayah = "Bantul",
                    TanggalUpdate = DateTime.Now
                }
            };
        }

        public void AddHargaBaru(string nama, decimal harga)
        {
            DaftarHarga.Add(new HargalkanModel()
            {
                Id = Guid.NewGuid().ToString(),
                NamaIkan = nama,
                HargaPerKilo = harga,
                TanggalUpdate = DateTime.Now,
                Wilayah = "Bantul"
            });
        }

        public void UpdateHarga(string id, string nama, decimal harga)
        {
            var item = DaftarHarga.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.NamaIkan = nama;
                item.HargaPerKilo = harga;
                item.TanggalUpdate = DateTime.Now;
            }
        }

        public void DeleteHarga(string id)
        {
            var item = DaftarHarga.FirstOrDefault(x => x.Id == id);
            if (item != null)
                DaftarHarga.Remove(item);
        }
    }
}
